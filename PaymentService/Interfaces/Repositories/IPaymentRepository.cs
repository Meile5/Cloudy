using PaymentService.Models;

namespace PaymentService.Interfaces.Repositories;

public interface IPaymentRepository
{
    public Task<List<Payment>> GetAllPayments();

    public Task<Payment?> GetPaymentById(string paymentId);

    public Task AddPayment(Payment payment);

    public Task UpdatePayment(Payment payment);

    public Task DeletePayment(string paymentId);
}