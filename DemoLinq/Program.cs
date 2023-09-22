// See https://aka.ms/new-console-template for more information
using Context;
using System.Collections;

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
Console.WriteLine("\nDistinct :\n");
//Lambda
var Prenoms = Contacts.Select(c => c.Prenom).Distinct();
var Prenoms2 = (from c in Contacts
               select c.Prenom).Distinct();
// Distinct n'est pas disponible en expression donc on utilise distinct sur le résultat de la requête (qui est une liste)

foreach(var prenom in Prenoms2)
{
    Console.WriteLine(prenom);
}
//Expression
#endregion

#region SingleOrDefault & FirstOrDefault
Console.WriteLine("\nSingleOrDefault & FirstOrDefault :\n");
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
Contact? thierry = (from c in Contacts
                    where c.Prenom.Equals("Thierry")
                    select c).FirstOrDefault();

// Si on veut passer un Default :
//Contact? thierry = (from c in Contacts
//                    where c.Prenom.Equals("Jean_Mich")
//                    select c).FirstOrDefault(new Contact { Prenom = "John", Nom = "Doe", AnneeDeNaissance = 1902, Email = "john.doe@yahoo.com" });

Console.WriteLine($"{thierry?.Prenom} {thierry?.Nom}");

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
IEnumerable<Contact> mesContacts = from c in Contacts
                                   orderby c.Prenom descending, c.Nom descending
                                   select c;
// Desc
//Lambda
//IEnumerable < Contact > mesContacts = Contacts.OrderByDescending(c => c.Nom);
//Expression
//IEnumerable<Contact> mesContacts = from c in Contacts
//                                   orderby c.Nom descending
//                                   select c;


foreach (Contact c in mesContacts)
{
    Console.WriteLine($"{c.Nom} {c.Prenom}");
}
#endregion
Console.WriteLine("\n\n\nFin");
Console.ReadLine();