using System.Data;
using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public BrazilianState State { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required string Neighborhood { get; set; }
        public required string Complement { get; set; }
        public required string Number { get; set; }
        public required string ZipCode { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal Total { get; set; }
        public IList<OrderItem> Items { get; set; } = [];


        // O método atualiza o status do pedido, validando se a transição é permitida pelo fluxo; lança DomainException se não for
        public void UpdateStatus(OrderStatus status)
        {
            switch (Status)
            {
                case OrderStatus.AwaitingPayment:
                    if(status != OrderStatus.Paid && status != OrderStatus.Cancelled)
                        throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
                    break;

                case OrderStatus.Paid:
                    if(status != OrderStatus.InPreparation && status != OrderStatus.Cancelled)
                        throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
                    break;

                case OrderStatus.InPreparation:
                    if(status != OrderStatus.Shipped && status != OrderStatus.Cancelled)
                        throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
                    break;

                case OrderStatus.Shipped:
                    if(status != OrderStatus.Delivered)
                        throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
                    break;

                case OrderStatus.Delivered:
                    if(status != OrderStatus.Returned)
                        throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
                    break;

                default:
                    throw new DomainException($"Não é possível mudar o pedido de {Status} para {status}.");
            }

            Status = status;
        }
    }
}