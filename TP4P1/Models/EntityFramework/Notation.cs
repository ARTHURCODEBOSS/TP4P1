using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP4P1.Models.EntityFramework
{
    [Table("t_j_notation_not")]
    [PrimaryKey(nameof(UtilisateurId), nameof(FilmId))]
    public class Notation
    {
        [Column("utl_id")]
        public int UtilisateurId { get; set; } 

        [Column("flm_id")]
        public int FilmId { get; set; } 

        [Column("not_note")]
        [Range(0, 5, ErrorMessage = "La note doit être comprise entre 0 et 5.")] 
        public int Note { get; set; }

       

        [ForeignKey(nameof(UtilisateurId))]
        [InverseProperty(nameof(Utilisateur.NotesUtilisateur))]
        public virtual Utilisateur UtilisateurNotant { get; set; } = null!;

        [ForeignKey(nameof(FilmId))]
        [InverseProperty(nameof(Film.NotesFilm))]
        public virtual Film FilmNote { get; set; } = null!; 
    }
}
