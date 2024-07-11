using System;
using System.Collections.Generic;
using System.Linq;
using LinQObrigadaProject.Contact;
using LinQObrigadaProject.Todo;
using Newtonsoft.Json;

namespace TodoManager
{

    public class TodoManager
    {
        private List<Todo> _todos;
        private const string FilePath = "DataSources/todo.txt";

        public TodoManager()
        {
            if (!File.Exists(FilePath))
            {
                _todos = new List<Todo>();
            }

            var lines = File.ReadAllLines(FilePath);
            _todos = lines.Select(line =>
            {
                var parts = line.Split(new[] { "::" }, StringSplitOptions.None);
                return new Todo { Label = parts[0], Completed = bool.Parse(parts[1]) };
            }).ToList();
        }
        
        public void AddTodo(string label)
        {
            _todos.Add(new Todo { Label = label, Completed = false});
            SaveTodos();
            Console.WriteLine("Nouvelle tâche ajoutée dans votre liste");
        }

        public void RemoveTodo(int index)
        {
            if (index >= 0 && index < _todos.Count)
            {
                _todos.RemoveAt(index);
                SaveTodos();
                Console.WriteLine("L'élément de votre TODO a été supprimé");
            }
            else
            {
                Console.WriteLine($"Cet élément n'a pas pu être trouvé dans votre TODO avec cete index {index}");
            }
        }

        public void MarkAsComple(int index)
        {
            if (index >= 0 && index < _todos.Count)
            {
                _todos[index].Completed = true;
                SaveTodos();
                Console.WriteLine("Cette tâche est désormais finie");
            }
            else
            {
                Console.WriteLine($"Tâche introuvable avec l'index {index}");
            }
        }
        
        public void SearchTodo(string query)
        {
            var searchResults = _todos.Where(t => t.Label.Contains(query, StringComparison.OrdinalIgnoreCase)
                                                 )
                .ToList();
            
            if (searchResults.Any())
            {
                Console.WriteLine("Résultat de votre recherche");
                Console.WriteLine($"Vous avez encore {searchResults.Count} dans votre liste");
                
                for (int index = 0; index < _todos.Count; index++)
                {
                    Todo todo = _todos[index];
                    string inProgress = todo.Completed ? "Terminer" : "En cours" ;
                    
                    Console.WriteLine($"{index} : {todo.Label} ({inProgress})");
                    Console.WriteLine("⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐");                
                }
            }
            else
            {
                Console.WriteLine($"Aucun élément trouvé avec votre recherche : {query}");
            }
        }

        public void ListTodos()
        {
            if (_todos.Any())
            {
                Console.WriteLine($"Nombre de tâche : {_todos.Count} ");
                
                for (int index = 0; index < _todos.Count; index++)
                {
                    Todo todo = _todos[index];

                    string inProgress = todo.Completed ? "Terminer" : "En cours" ;
                    
                    Console.WriteLine($"    {index} : {todo.Label} ({inProgress})");
                    Console.WriteLine("⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐ ⊏⊐");                
                }
            }
            else
            {
                Console.WriteLine("Vous n'avez pas d'action à faire");
            }
        }
        
        private void SaveTodos()
        {
            var lines = _todos.Select(todo => $"{todo.Label}::{todo.Completed}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}