using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;
namespace LoncotesLibrary.Models;

public class Checkout
{
    public int Id { get; set; }
    [Required]
    public int MaterialId { get; set; }
    public Material Material { get; set; }
    [Required]
    public int PatronId { get; set; }
    public Patron Patron { get; set; }
    [Required]
    public DateTime CheckoutDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    private static decimal _lateFeePerDay = .50M;
    public decimal? LateFee 
    {
        get
        {
            DateTime dueDate = CheckoutDate.AddDays(Material.MaterialType.CheckoutDays);
            // material has not been returned; 
            if (ReturnDate != null && dueDate < ReturnDate)
            {
                int daysLate = ((DateTime)ReturnDate - dueDate).Days;
                decimal fee = daysLate * _lateFeePerDay;
                return fee;
            }
            return null;
        }
    }
    public bool? Paid { get; set; }
}