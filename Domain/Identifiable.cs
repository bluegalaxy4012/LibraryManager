using System.ComponentModel.DataAnnotations;
namespace LibraryManager.Domain;

public abstract class Identifiable
{
    [Key]
    public int Id { get; set; }
}