using PaymentService.DTOs;
using PaymentService.Models;

namespace PaymentService.Interfaces.Services;

public interface IPaymentService
{
    public Task<List<Payment>> GetAllPayments();

    public  Task<Payment?> GetPaymentById(string paymentId);

    public Task AddPayment(CreatePaymentDto payment);

    public Task UpdatePayment(Payment payment);
    public Task RefundPayment(Payment payment);

    public Task CompletePayment(Payment payment);

    public Task DeletePayment(string paymentId);
}