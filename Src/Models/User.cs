

namespace TallerBackendIDWM.Src.Models
{
    public class User
    {
       public int Id {get; set;}
       public string Rut {get; set;} = string.Empty;
       public string Name {get; set;} = string.Empty;
       public DateTime Birthday {get; set;}
       public string Email {get; set;} = string.Empty;
       public string Password {get; set;} = string.Empty;

       //Relacion para asignar rol
       public int RoleId {get; set;}
       public Role Role {get; set;} = null!;

       //Relacion para asignar genero
       public int GenderId {get; set;}
       public Gender Gender {get; set;} = null!;
       
       //Permite ver si el usuario fue deshabilitado o no
       public bool IsActive {get; set;} 

    }
}