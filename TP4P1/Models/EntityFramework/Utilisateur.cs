using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP4P1.Models.EntityFramework
{
    [Table("t_e_utilisateur_utl")]
    [Index(nameof(Mail), IsUnique = true)] 
    public class Utilisateur
    {
        [Key]
        [Column("utl_id")]
        public int UtilisateurId { get; set; } 

        [Column("utl_nom")]
        [StringLength(50)]
        public string? Nom { get; set; }

        [Column("utl_prenom")]
        [StringLength(50)]
        public string? Prenom { get; set; }

        
        [Column("utl_mobile", TypeName = "char(10)")] 
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le mobile doit contenir 10 chiffres.")]
        public string? Mobile { get; set; }

        [Required]
        [Column("utl_mail")]
        [EmailAddress(ErrorMessage = "Le champ Mail n'est pas une adresse e-mail valide.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La longueur d'un email doit être comprise entre 6 et 100 caractères.")] // Contrainte de longueur combinée [cite: 869]
        public string Mail { get; set; } = null!; 

        [Required]
        [Column("utl_pwd")]
        [StringLength(64)]
        public string Pwd { get; set; } = null!;

        [Column("utl_rue")]
        [StringLength(200)]
        public string? Rue { get; set; }

        
        [Column("utl_cp", TypeName = "char(5)")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Le code postal doit contenir 5 chiffres.")]
        public string? CodePostal { get; set; }

        [Column("utl_ville")]
        [StringLength(50)]
        public string? Ville { get; set; }

        [Column("utl_pays")]
        [StringLength(50)]
        public string? Pays { get; set; } = "France";

        [Column("utl_latitude")]
        public float? Latitude { get; set; }

        [Column("utl_longitude")]
        public float? Longitude { get; set; }

        [Required]
        [Column("utl_datecreation", TypeName = "date")]
        public DateTime DateCreation { get; set; } = DateTime.Now; 

        // --- Propriété de navigation ---
        [InverseProperty(nameof(Notation.UtilisateurNotant))]
        public virtual ICollection<Notation> NotesUtilisateur { get; set; } = new List<Notation>();
    }
}
