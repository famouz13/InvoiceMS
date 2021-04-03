using AutoMapper;
using InvoiceMS.DAL;
using InvoiceMS.DataClients;
using InvoiceMS.Mapper;
using InvoiceMS.Models.DTOs;
using InvoiceMS.Models.DTOs.External.ShoppingCartMS;
using InvoiceMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceMS.Services {
  public class InvoicesService : IInvoiceService {
    private IMapper _mapper;
    private IInvoicesRepository _invoicesRepository;
    private IShoppingCartMsClient _shoppingCartMsClient;
    public InvoicesService(IInvoicesRepository invoicesRepository, IShoppingCartMsClient shoppingCartMsClient) {
      _mapper = AutoMapperConfig.Configure().CreateMapper();
      _invoicesRepository = invoicesRepository;
      _shoppingCartMsClient = shoppingCartMsClient;
    }


    public async Task<List<InvoiceDTO>> All() {
      var items = await _invoicesRepository.All();

      return _mapper.Map<List<InvoiceDTO>>(items);
    }

    public async Task<InvoiceDTO> ByUserID(int userID) {
      //получаем текущую корзину
      ShoppingCartDTO cartDTO = await _shoppingCartMsClient.GetShoppingCartByUserID(userID);

      //если она существует начинаем создание счета
      if (cartDTO != null && cartDTO.ShoppingCartID > 0 && cartDTO.Items.Count > 0) {
        Invoice invoiceFromDb = await _invoicesRepository.ByShoppingCartID(cartDTO.ShoppingCartID);

        if (invoiceFromDb != null) {
          //если актуальная информация не сходится с информацией в бд то удаляем старый счет и создаем новый иначе возвращаем уже имеющийся счет
          if (invoiceFromDb.Total != cartDTO.Total || invoiceFromDb.CountOfCommodities != cartDTO.Items.Count) {
            await _invoicesRepository.DeleteByID(invoiceFromDb.InvoiceID);

            Invoice newInvoice = CreateInvoiceFromCart(cartDTO);
            Invoice newInvoiceFromDb = await _invoicesRepository.Add(newInvoice);

            return _mapper.Map<InvoiceDTO>(newInvoiceFromDb);

          } else {
            return _mapper.Map<InvoiceDTO>(invoiceFromDb);
          }
          //если счета по айди корзины в базе нет то создаем новый
        } else {
          Invoice newInvoice = CreateInvoiceFromCart(cartDTO);
          Invoice newInvoiceFromDb = await _invoicesRepository.Add(newInvoice);

          return _mapper.Map<InvoiceDTO>(newInvoiceFromDb);
        }

      }

      return new InvoiceDTO();
    }

    private static Invoice CreateInvoiceFromCart(ShoppingCartDTO cartDTO) {
      return new Invoice() {
        UserID = cartDTO.UserID,
        ShoppingCartID = cartDTO.ShoppingCartID,
        CountOfCommodities = cartDTO.Items.Count,
        Total = cartDTO.Total,
        CreatedAt = DateTime.Now
      };
    }


  }
}
