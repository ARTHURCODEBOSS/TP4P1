using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP4P1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP4P1.Models.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TP4P1.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private FilmRatingsDBContext context;

        public FilmRatingsDBContext Context
        {
            get { return context; }
            set { context = value; }
        }

        public UtilisateursControllerTests()
        {
             Context= new FilmRatingsDBContext();
        }


        [TestMethod()]
        public void GetAllUtilisateursControllerTest()
        {
            UtilisateursController controller = new UtilisateursController(Context);
            var result = controller.GetUtilisateurs();
            List<Utilisateur> usersControl = result.Result.Value.ToList();

            List<Utilisateur> usersContext = context.Utilisateurs.ToList();

            CollectionAssert.AreEqual(usersContext, usersControl,"Les deux listes ne sont pas similaire");


        }

        [TestMethod()]
        public void GetUtilisateursByIdControllerTest()
        {
            UtilisateursController controller = new UtilisateursController(Context);

            Utilisateur users = controller.GetUtilisateurById(1).Result.Value;
            Utilisateur userContext = context.Utilisateurs.Find(1); 
            
            Assert.AreEqual(userContext, users, "Les deux utilisateurs ne sont pas similaire");


        }
        [TestMethod()]
        public void GetUtilisateursByIdControllerTestEchec()
        {
            UtilisateursController controller = new UtilisateursController(Context);

            var result = controller.GetUtilisateurById(100).Result;

            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundResult)result.Result).StatusCode, "pas le meme statut");
        }
        [TestMethod()]
        public void GetUtilisateursByEmailControllerTest()
        {
            UtilisateursController controller = new UtilisateursController(Context);

            var result = controller.GetUtilisateurByEmail("clilleymd@last.fm").Result.Value;
            Utilisateur userContext = context.Utilisateurs.FirstOrDefaultAsync(u => u.Mail == "clilleymd@last.fm").Result;

            Assert.AreEqual(result, userContext, "pas le utilisateur");
        }

        [TestMethod()]
        public void GetUtilisateursByEmailControllerTestEchec()
        {
            UtilisateursController controller = new UtilisateursController(Context);

            var result = controller.GetUtilisateurByEmail("kim@gmail.com").Result;

            Assert.AreEqual(StatusCodes.Status404NotFound, ((NotFoundResult)result.Result).StatusCode, "pas le meme statut");
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            UtilisateursController controller = new UtilisateursController(Context);

            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            
             Utilisateur userAtester = new Utilisateur()
             {
                 Nom = "MACHIN",
                 Prenom = "Luc",
                 Mobile = "0606070809",
                 Mail = "machin" + chiffre + "@gmail.com",
                 Pwd = "Toto1234!",
                 Rue = "Chemin de Bellevue",
                 CodePostal = "74940",
                 Ville = "Annecy-le-Vieux",
                 Pays = "France",
                 Latitude = null,
                 Longitude = null
             };
             var result = controller.PostUtilisateur(userAtester).Result;
             // Assert
             Utilisateur? userRecupere = context.Utilisateurs.Where(u => u.Mail.ToUpper() == userAtester.Mail.ToUpper()).FirstOrDefault(); 
             
             userAtester.UtilisateurId = userRecupere.UtilisateurId;
             Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void Pututilisateur_ModelValidated_CreationOK()
        {
            UtilisateursController controller = new UtilisateursController(Context);
            Utilisateur userAtester = context.Utilisateurs.First();

            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);

            userAtester.Mail = "Marchin" + chiffre + "@gmail.com";
            var result = controller.PutUtilisateur(userAtester.UtilisateurId, userAtester).Result;
            Utilisateur? userRecupere = context.Utilisateurs.FirstOrDefault(u => u.UtilisateurId == userAtester.UtilisateurId);
            
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }


    }
}