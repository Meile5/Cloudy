using PaymentService.DTOs;
using PaymentService.Interfaces.Repositories;
using PaymentService.Interfaces.Services;
using PaymentService.Models;

namespace PaymentService.Service;

public class PaymentService(IPaymentRepository repo) : IPaymentService
{
    public async Task<List<Payment>> GetAllPayments()
    {
        return await repo.GetAllPayments();
    }
    
    public async Task<Payment?> GetPaymentById(string paymentId)
    {
        return await repo.GetPaymentById(paymentId);
    }
    
    public async Task AddPayment(CreatePaymentDto payment)
    {
        var newPayment = CreatePaymentDto.toPayment(payment);
        await repo.AddPayment(newPayment);
    }
    
    public async Task UpdatePayment(Payment payment)
    {
        await repo.UpdatePayment(payment);
    }
    
    public async Task RefundPayment(Payment payment)
    {
        payment.Status = "refunded";
        await repo.UpdatePayment(payment);
    }
    
    public async Task CompletePayment(Payment payment)
    {
        payment.Status = "complete";
        await repo.UpdatePayment(payment);
    }
    
    public async Task DeletePayment(string paymentId)
    {
        await repo.DeletePayment(paymentId);
    }
}