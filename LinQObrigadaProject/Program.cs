using System;

namespace LinQObrigadaProject
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runningMiniTel = true;
            
            Console.WriteLine("Bienvenu sur votre MiniTel");

            while (runningMiniTel)
            {
                DisplayGeneralActions();
                Console.Write("Choisir une action : ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Contact();
                        break;
                    case "2":
                        Todo();
                        break;
                    case "3":
                        Console.WriteLine("A bientôt !");
                        runningMiniTel = false;
                        break;
                    case "4":
                        DisplayGeneralActions();
                        break;
                    default:
                        Console.WriteLine("Action non reconnue …");
                        break;
                }
    
                Console.WriteLine();
            }
        }

        static void DisplayGeneralActions()
        {
            Console.WriteLine("1. Accéder à vos contacts");
            Console.WriteLine("2. Accéder à votre TODO List");
            Console.WriteLine("3. Quitter votre MiniTel");
            Console.WriteLine("4. Pour revoir les actions disponibles ");
        }

        static void DisplayRepertoirActions()
        {
            Console.WriteLine("1. Ajouter un nouveau contact");
            Console.WriteLine("2. Supprimer un contact existant");
            Console.WriteLine("3. Rechercher un contact");
            Console.WriteLine("4. Afficher tous vos contacts");
            Console.WriteLine("5. Retour en arrière");
        }
        
        static void DisplayToDoActions()
        {
            Console.WriteLine("1. Ajouter un nouveau élément");
            Console.WriteLine("2. Supprimer un élément");
            Console.WriteLine("3. Finir une tâche");
            Console.WriteLine("4. Rechercher un élément");
            Console.WriteLine("5. Lister toutes mes tâches");
            Console.WriteLine("6. Retour en arrière");
        }
        
        static string AskForInformation()
        {
            string response = Console.ReadLine();

            while (response != null && response.Equals(""))
            {
                Console.Write($"Indiquer une valeur valide : ");
                response = Console.ReadLine();
            }
            
            return response;
        }

        static int GetIndex()
        {
            string response = AskForInformation();
            return Convert.ToInt32(response);
        }
        
        static void Contact()
        {
            Console.WriteLine("Vous êtes maintenant dans votre répertoire de contact");            
            bool runningContact = true;
            
            ContactManager.ContactManager contactManager = new ContactManager.ContactManager();
            
            while (runningContact)
            {         
                Console.WriteLine();
                DisplayRepertoirActions();
                Console.Write("Choisir une action : ");
                string action = Console.ReadLine();

                switch (action)
                {
                    case "1":
                        Console.Write("Indiquer un nom de contact : ");
                        string name = AskForInformation();
                        
                        Console.Write("Indiquer un numéro de téléphone : ");
                        string phoneNumber = AskForInformation();
                        
                        Console.Write("Indiquer une adresse email : ");
                        string email = AskForInformation();
                        
                        Console.Write("Voulez-vous ajouter ce contact dans vos favori ? (O/N) ");
                        string fav = AskForInformation();
                        bool favorite = fav.Equals("O") ? true : false;
                        
                        contactManager.AddContact(name, phoneNumber, email, favorite);
                        break;
                    case "2":
                        Console.Write("Entrer le nom du contact à supprimer : ");
                        string nameToRemove = AskForInformation();
                        contactManager.RemoveContact(nameToRemove);
                        break;
                    case "3":
                        Console.Write("Rechercher un contact (nom, téléphone ou email) : ");
                        string query = AskForInformation();
                        contactManager.SearchContact(query);
                        break;
                    case "4":
                        contactManager.ListContacts();
                        break;
                    case "5":
                        runningContact = false;
                        break;
                    default:
                        Console.WriteLine("Action non reconnue …");
                        break;
                }
            }
        }

        static void Todo()
        {
            Console.WriteLine();
            Console.WriteLine("Vous êtes maintenant dans votre TODO list");
            
            TodoManager.TodoManager todoManager = new TodoManager.TodoManager();
            bool runningTodo = true;
            
            while (runningTodo)
            {
                DisplayToDoActions();
                Console.Write("Choisir une action : ");
                string action = Console.ReadLine();
                int index;
                
                switch (action)
                {
                    case "1":
                        Console.Write($"Indiquer le nom de la tâche a réaliser : ");
                        string label = AskForInformation();
                        todoManager.AddTodo(label);
                        break;
                    case "2":
                        Console.Write($"Indiquer l'index de la tâche à supprimer : ");
                        index = GetIndex();
                        todoManager.RemoveTodo(index);
                        break;
                    case "3":
                        Console.Write($"Indiquer l'index de la tâche à terminer : ");
                        index = GetIndex();
                        todoManager.MarkAsComple(index);
                        break;
                    case "4":
                        Console.Write("Rechercher une tâche : ");
                        string query = AskForInformation();
                        todoManager.SearchTodo(query);
                        break;
                    case "5":
                        todoManager.ListTodos();
                        break;
                    case "6":
                        runningTodo = false;
                        break;
                    default:
                        Console.WriteLine("Action non reconnue …");
                        break; 
                }
            }
        }
    }
}
