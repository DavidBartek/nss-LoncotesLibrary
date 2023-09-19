using System.ComponentModel.DataAnnotations;
namespace LoncotesLibrary.Models;

public class Patron
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public bool IsActive { get; set; }
    public List<Checkout> Checkouts {get; set;}
    public decimal Balance
    {
        get
        {
            // calculated property: Balance
            // totals up unpaid fines that a patron owes
            // if Checkout.Paid == false: implement sum logic
            // else: return null

            // declare a running total variable
            // iterate through linked checkouts, using the following properties
            // if Paid = false, add LateFee to runningTotal

            decimal runningTotal = 0M;
            if (Checkouts != null)
            {
                foreach (Checkout Checkout in Checkouts)
                {
                    if (Checkout.Paid == false && Checkout.LateFee > 0)
                    {
                        runningTotal += (decimal)Checkout.LateFee;
                    }
                }
            }
            return runningTotal;
            
        }
    }
}