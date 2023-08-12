using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1;


public record DbOption
{
    //[Required]
    public required string DbConnRO { get; init; }
    //[Required]
    public required string DbConnRW { get; init; }
}
