// See https://aka.ms/new-console-template for more information
using Context;
using DemoLinq;
using System;
using System.Collections;
using System.Threading.Tasks.Dataflow;

List<Contact> Contacts = new List<Contact>();
Contacts.AddRange(new Contact[] {
    new Contact() { Nom = "Person", Prenom = "Michael", Email = "michael.person@cognitic.be", AnneeDeNaissance = 1982},
    new Contact() { Nom = "Morre", Prenom = "Thierry", Email = "thierry.morre@cognitic.be", AnneeDeNaissance = 1974},
    new Contact() { Nom = "Dupuis", Prenom = "Thierry", Email = "thierry.dupuis@cognitic.be", AnneeDeNaissance = 1988 },
    new Contact() {Nom = "Faulkner", Prenom = "Stéphane", Email = "stephane.faulkner@cognitic.be", AnneeDeNaissance = 1969 },
    new Contact() {Nom = "Selleck", Prenom = "Tom", Email = "tom.selleck@imdb.com", AnneeDeNaissance = 1945 },
    new Contact() { Nom = "Anderson", Prenom = "Richard Dean", Email = "richard.dean.anderson@imdb.com", AnneeDeNaissance = 1950 },
    new Contact() { Nom = "Bullock", Prenom = "Sandra", Email = "sandra.bullock@imdb.com", AnneeDeNaissance = 1964 },
    new Contact() { Nom = "Peppard", Prenom = "George", Email = "peppard.george@ateam.com", AnneeDeNaissance = 1928 },
    new Contact() {Nom = "Estevez", Prenom = "Emilio", Email = "emilio.estevez@breakfirstclub.com", AnneeDeNaissance = 1962 },
    new Contact() {Nom = "Moore", Prenom = "Demi", Email = "demi.moore@imdb.com", AnneeDeNaissance = 1962 },
    new Contact() {Nom = "Willis", Prenom = "Bruce", Email = "bruce.willis@diehard.com", AnneeDeNaissance = 1955}
});

List<RDV> RendezVous = new List<RDV>();
RendezVous.AddRange(new RDV[] {
    new RDV() { Email = "stephane.faulkner@cognitic.be", Date = new DateTime(2012, 5, 12) },
    new RDV() { Email = "peppard.george@ateam.com", Date = new DateTime(2011, 8, 14) },
    new RDV() { Email = "bruce.willis@diehard.com", Date = new DateTime(2012, 6, 19) },
    new RDV() { Email = "bruce.willis@diehard.com", Date = new DateTime(2012, 6, 20) },
    new RDV() { Email = "michael.person@cognitic.be", Date = new DateTime(2012, 04, 19) },
});

#region WHERE
Console.WriteLine("WHERE :\n");
// WHERE avec lambda
IEnumerable<Contact> MesContactsNesApres1970 = Contacts.Where(c => c.AnneeDeNaissance >= 1970);
IEnumerable<Contact> MesContactsNomLong = Contacts.Where(c => c.Nom.Length >= 8);

// WHERE avec expression
IEnumerable<Contact> MesContactsNesApres1970V2 = from c in Contacts
                                                where c.AnneeDeNaissance >= 1970
                                                select c;

foreach (Contact c in MesContactsNesApres1970V2)
{
    Console.WriteLine($"Nom : {c.Nom} | Prenom : {c.Prenom} | Année de Naissance : {c.AnneeDeNaissance}");
}
#endregion WHERE

#region SELECT
Console.WriteLine("\nSELECT :\n");
//SELECT lambda
var QuerySelectDemo = Contacts.Select(c => new { FullName = c.Prenom + " " + c.Nom, Courriel = c.Email });

//SELECT expression
var QuerySelectDemo2 = from c in Contacts
                       select new { FullName = c.Prenom + " " + c.Nom, Courriel = c.Email };

foreach (var cLight in QuerySelectDemo2)
{
    Console.WriteLine($"{cLight.FullName} : {cLight.Courriel}");
}
#endregion

#region DISTINCT
//Console.WriteLine("\nDistinct :\n");
////Lambda
//var Prenoms = Contacts.Select(c => c.Prenom).Distinct();

//var Prenoms2 = (from c in Contacts
//               select c.Prenom).Distinct();
// Distinct n'est pas disponible en expression donc on utilise distinct sur le résultat de la requête (qui est une liste)

//foreach(var prenom in Prenoms2)
//{
//    Console.WriteLine(prenom);
//}
//Expression
#endregion

#region SingleOrDefault & FirstOrDefault
//Console.WriteLine("\nSingleOrDefault & FirstOrDefault :\n");
// Lambda  
//Single

//Contact? thierry = Contacts.Where(c => c.Prenom.Equals("Thierry") && c.Nom.Equals("Morre")).SingleOrDefault();
//Contact? thierry = Contacts.Where(c => c.Prenom.Equals("Thierry")).SingleOrDefault(); 
// ↑ Va planter parce qu'on a plusieurs Thierry et Single ne fonctionne que si la liste sur laquelle est appelée Single contient un seul élément

// First
//Contact? thierry = Contacts.Where(c => c.Prenom.Equals("Thierry") && c.Nom.Equals("Morre")).FirstOrDefault();
//Contact? thierry = Contacts.Where(c => c.Prenom.Equals("Thierry")).FirstOrDefault();
// ↑ Thierry Morre
//Contact? thierry = Contacts.Where(c => c.Prenom.Equals("Thierry")).OrderBy(c => c.Nom).FirstOrDefault();
// ↑ Si on ordonne par nom de famille, le first devient Thierry Dupuis

// Expression
// Single
//Contact? thierry = (from c in Contacts
//                    where c.Prenom.Equals("Thierry")
//                    select c).SingleOrDefault(); 
// Va planter because plusieurs Thierry

//First
//Contact? thierry = (from c in Contacts
//                    where c.Prenom.Equals("Thierry")
//                    select c).FirstOrDefault();

// Si on veut passer un Default :
//Contact? thierry = (from c in Contacts
//                    where c.Prenom.Equals("Jean_Mich")
//                    select c).FirstOrDefault(new Contact { Prenom = "John", Nom = "Doe", AnneeDeNaissance = 1902, Email = "john.doe@yahoo.com" });

//Console.WriteLine($"{thierry?.Prenom} {thierry?.Nom}");

#endregion

#region OrderBy
Console.WriteLine("\nOrderBy\n");
// -------------- SUR UNE SEULE COL
// Asc
//Lamda
//IEnumerable<Contact> mesContacts = Contacts.OrderBy(c => c.Nom);
//Expression
//IEnumerable<Contact> mesContacts = from c in Contacts
//                                   orderby c.Nom
//                                   select c;
// Desc
//Lambda
//IEnumerable < Contact > mesContacts = Contacts.OrderByDescending(c => c.Nom);
//Expression
//IEnumerable<Contact> mesContacts = from c in Contacts
//                                   orderby c.Nom descending
//                                   select c;

// ---------------------------------------------------
// --------------- SUR PLUSIEURS COLONNES
// Asc
//Lamda
//IEnumerable<Contact> mesContacts = Contacts.OrderBy(c => c.Prenom).ThenByDescending(c => c.Nom);
//Expression
//IEnumerable<Contact> mesContacts = from c in Contacts
//                                   orderby c.Prenom, c.Nom descending
//                                   select c;
// Desc
//Lambda
//IEnumerable < Contact > mesContacts = Contacts.OrderByDescending(c => c.Nom);
//Expression
//IEnumerable<Contact> mesContacts = from c in Contacts
//                                   orderby c.Nom descending
//                                   select c;


//foreach (Contact c in mesContacts)
//{
//    Console.WriteLine($"{c.Nom} {c.Prenom}");
//}
#endregion

#region Count
//Console.WriteLine("\n\nCount :\n");
//int NbContacts = Contacts.Count();
//int NbThierry = Contacts.Count(c => c.Prenom.Equals("Thierry"));
//long NbContactsBcp = Contacts.LongCount();

//Console.WriteLine($"Il y a {NbContacts} contacts dans notre liste");
//Console.WriteLine($"Il y a {NbThierry} Thierry dans notre liste");

#endregion

#region Min & Max
//Console.WriteLine("\n\nMin & Max :\n");
//int MinYear = Contacts.Min(c => c.AnneeDeNaissance);
//int MaxYear = Contacts.Max(c => c.AnneeDeNaissance);

//Console.WriteLine($"Le plus vieux est né en {MinYear} et le plus jeune en {MaxYear}");
#endregion

#region Sum
//Console.WriteLine("\n\nSum :\n");
//int SumAges = Contacts.Sum(c => DateTime.Now.Year - c.AnneeDeNaissance);

//Console.WriteLine($"La somme de tous les âges est { SumAges }");
#endregion

#region Average
//Console.WriteLine("\n\nAverage :\n");
//double AverageAges = Contacts.Average(c => DateTime.Now.Year - c.AnneeDeNaissance);

//Console.WriteLine($"La moyenne de tous les âges est {AverageAges}");

#endregion

#region GroupBy
Console.WriteLine("\n\nGroup By\n");
// Le classique : 
//On fait un groupBy sur les nomdomaine des email (ex cognitic.be)
IEnumerable<IGrouping<string, Contact>> ContactsByMail = Contacts.GroupBy(c => c.Email.Substring(c.Email.IndexOf("@") + 1));

// Expression :
IEnumerable<IGrouping<string, Contact>> ContactsByMailV2 = from c in Contacts
                                                           group c by c.Email.Substring                                             (c.Email.IndexOf("@") + 1);


//foreach( IGrouping<string, Contact> groupContact in ContactsByMailV2)
//{
//    // contact.Key -> Chacun des groupes du GroupBy

//    Console.WriteLine($"Pour le groupe {groupContact.Key} - {groupContact.Count()} : ");
//    foreach(Contact contact in groupContact )
//    {
//        Console.WriteLine(contact.Prenom + " " + contact.Nom);
//    }
//    Console.WriteLine();
//}

// Avec un type anonyme : 
// Le select devra toujours se trouver AVANT le GroupBy
// La colonne sur laquelle vous voulez faire votre GroupBy, devra se trouver dans le Select
var QueryResultMail = Contacts
    .Select(c => new { Courriel = c.Email, NomComplet = c.Prenom + " " + c.Nom })
    .GroupBy(c => c.Courriel.Substring(c.Courriel.IndexOf("@") + 1));

// En Expression :
// On doit d'abord faire la requête avec le select, qui va renvoyer un nouveau tableau, puis faire une requête dans ce tableau pour faire le GroupBy
var QueryResultMailV2 = from newc in (from c in Contacts
                                      select new { Courriel = c.Email, NomComplet = c.Prenom + " " + c.Nom })
                        group newc by newc.Courriel.Substring(newc.Courriel.IndexOf("@") + 1);

foreach (var groupResult  in QueryResultMailV2)
{
    Console.WriteLine($"Pour le groupe {groupResult.Key} - {groupResult.Count()} : ");
    foreach (var result in groupResult)
    {
        Console.WriteLine(result.NomComplet);
    }
    Console.WriteLine();
}


#endregion

#region Join
// Collection1.Join(Collection2, clef1 (lambda), clef2 (lambda), lambda (infosCollection1, infosCollection2) => { Select })
var ContactAndRDV = Contacts.Join(RendezVous, c => c.Email, rdv => rdv.Email, (c, rdv) => new { FullName = c.Prenom + " " + c.Nom, c.Email, Date = rdv.Date.ToShortDateString() });

// Expression
var ContactAndRDV2 = from c in Contacts
                     join rdv in RendezVous on c.Email equals rdv.Email
                     select new { FullName = c.Prenom + " " + c.Nom, c.Email, Date = rdv.Date.ToShortDateString() };

foreach(var contact in ContactAndRDV2)
{
    Console.WriteLine($"{contact.FullName} (email : {contact.Email}) a rdv le {contact.Date} ");
}
#endregion

#region GroupJoin
Console.WriteLine("\n\nGroup Join :\n");
var ContactAndRDVIfExist = Contacts.GroupJoin(RendezVous, c => c.Email, rdv => rdv.Email, (c, rdvs) => new { FullName = c.Prenom + " " + c.Nom, c.Email, RendezVous = rdvs });

var ContactAndRDVIfExist2 = from c in Contacts
                            join rdv in RendezVous on c.Email equals rdv.Email into rdvs
                            select new { FullName = c.Prenom + " " + c.Nom, c.Email, RendezVous = rdvs };

foreach(var contact in ContactAndRDVIfExist2)
{
    Console.WriteLine($"{contact.FullName} - {contact.Email}");
    if(contact.RendezVous.Count() > 0)
    {
        foreach(var rdv in contact.RendezVous)
        {
            Console.WriteLine($"\tRendez-vous le {rdv.Date.ToShortDateString()}");
        }
    }
    else
    {
        Console.WriteLine("\tPas de rendez-vous");
    }
}

#endregion

#region Multiple From
var CrossJoin = from c in Contacts
                from rdv in RendezVous
                select new { c.Email, rdv.Date };

foreach(var contact in CrossJoin)
{
    Console.WriteLine(contact);
}
                #endregion

Console.WriteLine("\n\n\nFin");
Console.ReadLine();