using System;
using System.Collections.Generic;
using System.Linq;
using LinQObrigadaProject.Contact;
using Newtonsoft.Json;

namespace ContactManager
{

    public class ContactManager
    {
        private List<Contact> _contacts;
        private const string FilePath = "DataSources/contacts.json";

        public ContactManager()
        {
            if (!File.Exists(FilePath))
            {
                _contacts = new List<Contact>();
            }
            else
            {
                var json = File.ReadAllText(FilePath);
                _contacts = JsonConvert.DeserializeObject<List<Contact>>(json);

            }
        }
        
        public void AddContact(string name, string phoneNumber, string email, bool favorite = false)
        {
            _contacts.Add(new Contact { Name = name, PhoneNumber = phoneNumber, Email = email, Favorite = favorite});
            SaveContacts();
            Console.WriteLine("Nouveau contact ajouté dans votre répertoire");
        }

        public void RemoveContact(string name)
        {
            var contact = _contacts.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (contact != null)
            {
                _contacts.Remove(contact);
                SaveContacts();
                Console.WriteLine("Le contact a été supprimé de votre répertoire");
            }
            else
            {
                Console.WriteLine("Le contact n'a pas pu être trouvé dans votre répertoire");
            }
        }

        public void SearchContact(string query)
        {
            var searchResults = _contacts.Where(c => c.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
                                                     || c.PhoneNumber.Contains(query)
                                                     || c.Email.Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();
            if (searchResults.Any())
            {
                Console.WriteLine("Résultat de votre recherche");
                Console.WriteLine($"Nombre de résultat : {searchResults.Count} ");
                foreach (var contact in searchResults)
                {
                    Console.WriteLine($"  Nom: {contact.Name}");
                    Console.WriteLine($"  Téléphone: {contact.PhoneNumber}");
                    Console.WriteLine($"  Email: {contact.Email}");
                    Console.WriteLine($"  Contact favori: {contact.Favorite}");
                    Console.WriteLine("⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐");
                }
            }
            else
            {
                Console.WriteLine($"Aucun contact trouvé avec votre recherche : {query}");
            }
        }

        public void ListContacts()
        {
            if (_contacts.Any())
            {
                Console.WriteLine("List de vos contacts dans votre répertoire :");
                Console.WriteLine($"Nombre de contact : {_contacts.Count} ");
                foreach (var contact in _contacts)
                {
                    Console.WriteLine($"  Nom: {contact.Name}");
                    Console.WriteLine($"  Téléphone: {contact.PhoneNumber}");
                    Console.WriteLine($"  Email: {contact.Email}");
                    Console.WriteLine($"  Contact favori: {contact.Favorite}");
                    Console.WriteLine("⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐");
                }
            }
            else
            {
                Console.WriteLine("Vous n'avez pas de contact dans votre répertoire");
            }
        }
        
        private void SaveContacts()
        {
            var json = JsonConvert.SerializeObject(_contacts, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
    }
}