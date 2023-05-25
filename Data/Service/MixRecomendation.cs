namespace EReaderNow.Data.Service
{
    public class MixRecomendation
    {


        class CollaborativeFiltering
        {   // Функция чтения файла 
            // Create a list of books in the library
            public void sss()
            {
                var books = new List<Book>()
            {
                new Book("The Great Gatsby", "F. Scott Fitzgerald", "Novel", 1925),
                new Book("Pride and Prejudice", "Jane Austen", "Novel", 1813),
                new Book("To Kill a Mockingbird", "Harper Lee", "Novel", 1960),
                new Book("1984", "George Orwell", "Dystopian fiction", 1949),
                new Book("The Catcher in the Rye", "J.D. Salinger", "Novel", 1951),
                new Book("The Hobbit", "J.R.R. Tolkien", "Fantasy", 1937),
                new Book("The Lord of the Rings", "J.R.R. Tolkien", "Fantasy", 1954)
            };

                // Create a list of users and their favorite genres
                var users = new List<User>()
            {
                new User("Alice", new List<string>() {"Novel", "Dystopian fiction"}),
                new User("Bob", new List<string>() {"Fantasy"}),
                new User("Charlie", new List<string>() {"Novel"}),
                new User("Dave", new List<string>() {"Dystopian fiction", "Novel"}),
                new User("Eve", new List<string>() {"Fantasy"})
            };

                // Recommend books to each user based on their favorite genres
                foreach (var user in users)
                {
                    Console.WriteLine("Recommended books for {0}:", user.Name);
                    foreach (var book in books)
                    {
                        if (user.FavoriteGenres.Contains(book.Genre))
                        {
                            Console.WriteLine("- {0} by {1} ({2})", book.Title, book.Author, book.Year);
                        }
                    }
                    Console.WriteLine();
                }
            }
            public Recommendation[] GetMixedRecommendations(RecommendationSystem system)
            {
                // Получаем два массива рекомендаций из двух методов системы рекомендаций
                Recommendation[] recommendations1 = system.GetRecommendationsMethod1();
                Recommendation[] recommendations2 = system.GetRecommendationsMethod2();

                // Создаем новый массив для хранения смешанных рекомендаций
                Recommendation[] mixedRecommendations = new Recommendation[10];

                // Создаем массив для отслеживания занятых позиций в массиве смешанных рекомендаций
                bool[] occupiedPositions = new bool[10];

                // Обрабатываем каждую рекомендацию из первого массива
                for (int i = 0; i < 10; i++)
                {
                    // Ищем соответствующую рекомендацию во втором массиве
                    int indexInSecondArray = Array.IndexOf(recommendations2, recommendations1[i]);

                    // Если рекомендация найдена во втором массиве
                    if (indexInSecondArray != -1)
                    {
                        // Вычисляем позицию для этой рекомендации в массиве смешанных рекомендаций
                        int mixedPosition = (i + indexInSecondArray) / 2;

                        // Проверяем, свободна ли эта позиция
                        if (!occupiedPositions[mixedPosition])
                        {
                            // Если позиция свободна, добавляем рекомендацию в массив смешанных рекомендаций
                            mixedRecommendations[mixedPosition] = recommendations1[i];
                            occupiedPositions[mixedPosition] = true;
                        }
                        else
                        {
                            // Если позиция занята, ищем ближайшую свободную позицию сверху или снизу
                            int offset = 1;
                            bool positionFound = false;
                            while (!positionFound && (mixedPosition - offset >= 0 || mixedPosition + offset < 10))
                            {
                                if (mixedPosition - offset >= 0 && !occupiedPositions[mixedPosition - offset])
                                {
                                    mixedRecommendations[mixedPosition - offset] = recommendations1[i];
                                    occupiedPositions[mixedPosition - offset] = true;
                                    positionFound = true;
                                }
                                else if (mixedPosition + offset < 10 && !occupiedPositions[mixedPosition + offset])
                                {
                                    mixedRecommendations[mixedPosition + offset] = recommendations1[i];
                                    occupiedPositions[mixedPosition + offset] = true;
                                    positionFound = true;
                                }
                                offset++;
                            }
                        }
                    }
                }

                // Возвращаем массив смешанных рекомендаций
                return mixedRecommendations;
            }
            public static Dictionary<int, Dictionary<int, float>> ReadFile(string filename = "csv_file.cvv")
            {
                Console.WriteLine("Чтение файла " + filename);
                var mentions = new Dictionary<int, Dictionary<int, float>>();
                using (var reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(',');
                        var user = Int32.Parse(values[0]);
                        var product = Int32.Parse(values[1]);
                        var rate = float.Parse(values[2]);
                        if (!mentions.ContainsKey(user))
                        {
                            mentions[user] = new Dictionary<int, float>();
                        }
                        mentions[user][product] = rate;
                    }
                }
                return mentions;
            }
            //Функция получения Косинусовой меры
            public static double DistCosine(Dictionary<int, float> vecA, Dictionary<int, float> vecB)
            {
                float dotProduct(Dictionary<int, float> vector1, Dictionary<int, float> vector2)
                {
                    float d = 0.0f;

                    foreach (var dim in vector1.Keys)
                    {
                        if (vector2.ContainsKey(dim))
                        {
                            d += vector1[dim] * vector2[dim];
                        }
                    }
                    return d;
                }
                float numerator = dotProduct(vecA, vecB);
                double denominator = Math.Sqrt(dotProduct(vecA, vecA)) * Math.Sqrt(dotProduct(vecB, vecB));
                return numerator / denominator;
            }

            //Функция получения Коэффициент корреляции Пирсона

            public static double PearsonCorrelation(Dictionary<int, float> ratings1, Dictionary<int, float> ratings2)
            {
                // Get the common keys (i.e., the books that both users have rated)
                var commonKeys = ratings1.Keys.Intersect(ratings2.Keys);

                // Calculate the sum of ratings and the sum of squared ratings for both users
                double sum1 = 0.0, sum2 = 0.0, sumSq1 = 0.0, sumSq2 = 0.0;

                foreach (int key in commonKeys)
                {
                    sum1 += ratings1[key];
                    sum2 += ratings2[key];
                    sumSq1 += Math.Pow(ratings1[key], 2);
                    sumSq2 += Math.Pow(ratings2[key], 2);
                }

                int n = commonKeys.Count();

                // Calculate the mean ratings for both users
                double mean1 = sum1 / n;
                double mean2 = sum2 / n;

                // Calculate the numerator and denominator for the Pearson Correlation Coefficient
                double numerator = 0.0, denominator1 = 0.0, denominator2 = 0.0;

                foreach (int key in commonKeys)
                {
                    double r1 = ratings1[key] - mean1;
                    double r2 = ratings2[key] - mean2;

                    numerator += r1 * r2;
                    denominator1 += Math.Pow(r1, 2);
                    denominator2 += Math.Pow(r2, 2);
                }

                double denominator = Math.Sqrt(denominator1 * denominator2);

                // If the denominator is 0, return 0 (since there is no correlation)
                if (denominator == 0)
                {
                    return 0.0;
                }

                // Calculate the Pearson Correlation Coefficient
                double r = numerator / denominator;

                return r;
            }
            //Функция получения Евклидово расстояние
            public static double DistEuclidean(Dictionary<int, float> vecA, Dictionary<int, float> vecB)
            {
                double sumSquares = 0;

                foreach (var dim in vecA.Keys)
                {
                    if (vecB.ContainsKey(dim))
                    {
                        sumSquares += Math.Pow(vecA[dim] - vecB[dim], 2);
                    }
                }

                return Math.Sqrt(sumSquares);
            }
            /*   static float DotProduct(Dictionary<int, float> vecA, Dictionary<int, float> vecB)
               {
               float d = 0.0;
               foreach (KeyValuePair<int, float> itemA in vecA)
                   {
                   if (vecB.ContainsKey(itemA.Key))
                       {
                       d += itemA.Value * vecB[itemA.Key];
                       }
                   }
               return d;
               }
       */
            /*   static float DistCosine(Dictionary<int, float> vecA, Dictionary<int, float> vecB)
               {
                   float dotProductAB = DotProduct(vecA, vecB);
                   float dotProductAA = DotProduct(vecA, vecA);
                   float dotProductBB = DotProduct(vecB, vecB);
                   return dotProductAB / Math.Sqrt(dotProductAA) / Math.Sqrt(dotProductBB);
               }*/

            //Коллаборативная фильтрация
            public static List<Tuple<int, float>> MakeRecommendation(int userID, Dictionary<int, Dictionary<int, float>> userRates, int nBestUsers, int nBestProducts)
            {
                var matches = new List<Tuple<int, float>>();
                foreach (var item in userRates)
                {
                    if (item.Key != userID)
                    {
                        float similarity = (float)DistCosine(userRates[userID], item.Value);
                        matches.Add(new Tuple<int, float>(item.Key, similarity));
                    }
                }
                var bestMatches = matches.OrderByDescending(x => x.Item2).Take(nBestUsers).ToList();
                Console.WriteLine("Most correlated with '{0}' users:", userID);
                foreach (var line in bestMatches)
                {
                    Console.WriteLine("  UserID: {0,-6}  Coeff: {1,-6:F4}", line.Item1, line.Item2);
                }
                var sim = new Dictionary<int, float>();
                float sim_all = bestMatches.Sum(x => x.Item2);
                bestMatches = bestMatches.Where(x => x.Item2 > 0.0).ToList();
                foreach (var relatedUser in bestMatches)
                {
                    foreach (var product in userRates[relatedUser.Item1])
                    {
                        if (!userRates[userID].ContainsKey(product.Key))
                        {
                            if (!sim.ContainsKey(product.Key))
                            {
                                sim[product.Key] = 0.0f;
                            }
                            sim[product.Key] += product.Value * relatedUser.Item2;
                        }
                    }
                }
                var bestProducts = sim.OrderByDescending(x => x.Value).Take(nBestProducts).ToList();
                Console.WriteLine("Most correlated products:");
                foreach (var prodInfo in bestProducts)
                {
                    Console.WriteLine("  ProductID: {0,-6}  CorrelationCoeff: {1,-6:F4}", prodInfo.Key, prodInfo.Value);
                }
                return bestProducts.Select(x => Tuple.Create(x.Key, x.Value)).ToList();
            }

        }
        class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Genre { get; set; }
            public int Year { get; set; }

            public Book(string title, string author, string genre, int year)
            {
                Title = title;
                Author = author;
                Genre = genre;
                Year = year;
            }
        }

        class User
        {
            public string Name { get; set; }
            public List<string> FavoriteGenres { get; set; }
            public List<int> RatedBookIds { get; set; }
            public User(string name, List<string> favoriteGenres)
            {
                Name = name;
                FavoriteGenres = favoriteGenres;
            }
        }


        class Rating
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
            public int Value { get; set; }
        }


        static void Main(string[] args)
        {   //Нужна базовая матрица(Массив)
            var matrix = new Dictionary<int, Dictionary<int, float>>();
            var matrix_Cosine = new List<Tuple<int, float>>();
            short userID = 1;
            short nBestUsers = 5;
            short nBestProducts = 5;
            //словарь рейтингов пользователей (userRates)
            matrix = CollaborativeFiltering.ReadFile();
            //количество наиболее похожих пользователей (nBestUsers) и количество рекомендуемых продуктов (nBestProducts).
            matrix_Cosine = CollaborativeFiltering.MakeRecommendation(userID, matrix, nBestUsers, nBestProducts);





        }


    }
}
