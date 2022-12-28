using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GuestBookTry2
{
    public class Posts
    {
        private string author = "";
        private string guestpost = "";
        public string Author
        { get { return author; }
            set { author = value; } }
        public string Guestpost
        { get { return guestpost; }
            set { guestpost = value;
            } }

        public class GuestBook
        {
            private string filename = @"guestbook.json";
            private List<Posts> posts = new List<Posts>();

            public GuestBook()
            {
                if (File.Exists(filename) == true)
                {
                    string jsonString = File.ReadAllText(filename);
                    posts = JsonSerializer.Deserialize<List<Posts>>(jsonString);
                }
            }

            public Posts addPost(string aut, string gbpost)
            {
                Posts obj = new Posts();
                obj.Author = aut;
                obj.Guestpost = gbpost;
                posts.Add(obj);
                marshal();
                return obj;
            }

            public int delPost(int index)
            {
                posts.RemoveAt(index);
                marshal();
                return index;
            }

            public List<Posts> GetPosts()
            {
                return posts;
            }

            private void marshal()
            {
                
                var jsonString = JsonSerializer.Serialize(posts);
                Console.WriteLine("::" + jsonString);
                File.WriteAllText(filename, jsonString);
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                GuestBook guestbook = new GuestBook();
                int i = 0;

                while (true)
                {
                    Console.Clear(); Console.CursorVisible = false;
                    Console.WriteLine("------ MATILDAS GÄSTBOK ------");
                    Console.WriteLine(" ");
                    Console.WriteLine("1. Skriv i gästboken");
                    Console.WriteLine("2. Ta bort inlägg");
                    Console.WriteLine(" ");
                    Console.WriteLine("X. Avsluta");

                    i = 0;
                    foreach (Posts posts in guestbook.GetPosts())
                    {
                        Console.WriteLine("[" + i++ + "]" + posts.Author + " - " + posts.guestpost);
                    }

                    int inp = (int)Console.ReadKey(true).Key;
                    switch (inp)
                    {
                        case '1':
                            Console.CursorVisible = true;
                            Console.Write("Skriv en författare på inlägget:");
                            string inputAuthor = Console.ReadLine();
                            if (string.IsNullOrEmpty(inputAuthor))
                            {
                                Console.WriteLine("Fältet kan inte vara tomt. Tryck på valfri knapp för att återgå.");
                                Console.ReadLine();
                                Console.Clear();
                            }
                            else
                            {
                            
                                Console.WriteLine("Skriv gästbokinlägg:");


                                string inputPost = Console.ReadLine();
                                if (string.IsNullOrEmpty(inputPost))
                                {
                                    Console.WriteLine("Fältet kan inte vara tomt. Tryck på valfri knapp för att återgå.");
                                    Console.ReadLine();
                                    Console.Clear();
                                }
                                else
                                {
                                    /* Placerar inläggets innehåll på vår indexplats 1 i array posts */
                                    guestbook.addPost(inputAuthor, inputPost);

                                
                                }

                                

                            }

                            break;
                            case '2':
                            Console.CursorVisible = true;
                            Console.Write("Ange index att radera: ");
                            string index = Console.ReadLine();
                            if (!String.IsNullOrEmpty(index))
                                try
                                {
                                    guestbook.delPost(Convert.ToInt32(index));
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Index out of range!\nPress button to proceed.");
                                    Console.ReadKey();
                                }
                            break;

                        case 88:
                            Environment.Exit(0);
                            break;
                    }
                }
            }
        } 
    }
}
