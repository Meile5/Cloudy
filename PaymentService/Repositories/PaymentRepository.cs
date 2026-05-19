using Microsoft.EntityFrameworkCore;
using PaymentService.Database;
using PaymentService.Interfaces.Repositories;
using PaymentService.Models;

namespace PaymentService.Repositories;

public class PaymentRepository(PaymentContext context) : IPaymentRepository
{
    public async Task<List<Payment>> GetAllPayments()
    {
        return await context.Payments.ToListAsync();
    }
    
    public async Task<Payment?> GetPaymentById(string paymentId)
    {
        return await context.Payments.Where(p => p.Id == paymentId).FirstOrDefaultAsync();
    }
    
    public async Task AddPayment(Payment payment)
    {
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
    }
    
    public async Task UpdatePayment(Payment payment)
    {
        context.Payments.Update(payment);
        await context.SaveChangesAsync();
    }
    
    public async Task DeletePayment(string paymentId)
    {
        var toDelete = await context.Payments.Where(p => p.Id == paymentId).FirstOrDefaultAsync();
        if (toDelete != null) context.Payments.Remove(toDelete);
        await context.SaveChangesAsync();
    }
}