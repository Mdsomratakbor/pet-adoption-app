using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data.Entities;

namespace PetAdoption.Api.Data
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions<PetContext> options) : base(options)
        {


        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavorites> UserFavorites { get; set; }
        public DbSet<UserAdoption> UserAdoption { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

#if DEBUG
            optionsBuilder.LogTo(Console.WriteLine);
#endif
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserFavorites>()
                .HasKey(uf => new { uf.UserId, uf.PetId });
            modelBuilder.Entity<Pet>()
                .HasData(InitialPetsData());
        }

        private static List<Pet> InitialPetsData()
        {
            var pets = new List<Pet>()
            {
                new Pet()
{
    Id = 1,
    Name = "Buddy",
    Breed = "Dog - Golden Retriever",
    Price = 300,
    Description = "Buddy is a loyal and friendly Golden Retriever. He enjoys playing fetch, going for long walks, and cuddling with his family."
},
new Pet()
{
    Id = 2,
    Name = "Whiskers",
    Breed = "Cat - Siamese",
    Price = 150,
    Description = "Whiskers is a sleek and elegant Siamese cat. He enjoys lounging in sunny spots, chasing toys, and receiving affection from his human companions."
},
new Pet()
{
    Id = 3,
    Name = "Rocky",
    Breed = "Dog - German Shepherd",
    Price = 400,
    Description = "Rocky is a brave and intelligent German Shepherd. He is highly trainable, loyal, and protective of his family. Rocky enjoys obedience training, playing games, and going on outdoor adventures."
},
new Pet()
{
    Id = 4,
    Name = "Max",
    Breed = "Dog - Labrador Retriever",
    Price = 350,
    Description = "Max is a friendly Labrador Retriever with a love for fetching. He enjoys spending time outdoors and playing with his favorite toys."
},
new Pet()
{
    Id = 5,
    Name = "Daisy",
    Breed = "Dog - Beagle",
    Price = 250,
    Description = "Daisy is a curious Beagle with a nose for adventure. She loves exploring new scents and going on long walks with her family."
},
new Pet()
{
    Id = 6,
    Name = "Luna",
    Breed = "Dog - Siberian Husky",
    Price = 500,
    Description = "Luna is an energetic Siberian Husky who thrives in cold climates. She enjoys running, hiking, and pulling sleds in the snow."
},
new Pet()
{
    Id = 7,
    Name = "Charlie",
    Breed = "Dog - Poodle",
    Price = 200,
    Description = "Charlie is an elegant Poodle known for his intelligence and grace. He enjoys learning new tricks and showing off his skills."
},
new Pet()
{
    Id = 8,
    Name = "Bailey",
    Breed = "Dog - Boxer",
    Price = 450,
    Description = "Bailey is a playful Boxer with a heart of gold. He loves romping around in the yard and chasing after balls with his boundless energy."
},
new Pet()
{
    Id = 9,
    Name = "Bella",
    Breed = "Dog - Shih Tzu",
    Price = 300,
    Description = "Bella is a charming Shih Tzu known for her affectionate nature. She enjoys cuddling on the couch and being pampered with grooming sessions."
},
new Pet()
{
    Id = 10,
    Name = "Cooper",
    Breed = "Dog - Border Collie",
    Price = 380,
    Description = "Cooper is a friendly and intelligent Border Collie. He loves playing fetch and going for long walks."
},
new Pet()
{
    Id = 11,
    Name = "Lucy",
    Breed = "Dog - Dachshund",
    Price = 280,
    Description = "Lucy is a playful Dachshund with a big heart. She enjoys cuddling on the couch and exploring new places."
},
new Pet()
{
    Id = 12,
    Name = "Toby",
    Breed = "Dog - Bulldog",
    Price = 420,
    Description = "Toby is a loyal Bulldog who adores his family. He enjoys napping in the sun and chewing on his favorite toys."
},
new Pet()
{
    Id = 13,
    Name = "Molly",
    Breed = "Dog - Cocker Spaniel",
    Price = 320,
    Description = "Molly is a gentle Cocker Spaniel who loves being pampered. She enjoys baths, brushing, and snuggling up with her humans."
},
new Pet()
{
    Id = 14,
    Name = "Oscar",
    Breed = "Dog - Rottweiler",
    Price = 450,
    Description = "Oscar is a strong and protective Rottweiler. He is great with kids and loves going on adventures in the great outdoors."
},
new Pet()
{
    Id = 15,
    Name = "Zoe",
    Breed = "Dog - Shetland Sheepdog",
    Price = 350,
    Description = "Zoe is a clever Shetland Sheepdog who excels in agility training. She is always up for a challenge and loves learning new tricks."
},
new Pet()
{
    Id = 16,
    Name = "Sam",
    Breed = "Dog - Australian Shepherd",
    Price = 500,
    Description = "Sam is an energetic Australian Shepherd who thrives on mental and physical stimulation. He enjoys herding and playing frisbee."
},
new Pet()
{
    Id = 17,
    Name = "Riley",
    Breed = "Dog - Great Dane",
    Price = 600,
    Description = "Riley is a majestic Great Dane with a gentle demeanor. Despite his size, he is a big softie who loves cuddling on the couch."
},
new Pet()
{
    Id = 18,
    Name = "Lola",
    Breed = "Dog - Pembroke Welsh Corgi",
    Price = 380,
    Description = "Lola is a charismatic Pembroke Welsh Corgi who charms everyone she meets. She enjoys herding and participating in dog sports."
},
new Pet()
{
    Id = 19,
    Name = "Bentley",
    Breed = "Dog - Bernese Mountain Dog",
    Price = 550,
    Description = "Bentley is a friendly and affectionate Bernese Mountain Dog. He loves snowy days and pulling carts or sleds."
},
new Pet()
{
    Id = 20,
    Name = "Sophie",
    Breed = "Dog - Cavalier King Charles Spaniel",
    Price = 400,
    Description = "Sophie is a sweet and gentle Cavalier King Charles Spaniel. She adores cuddling and will follow her humans everywhere."
},
new Pet()
{
    Id = 21,
    Name = "Zeus",
    Breed = "Dog - Doberman Pinscher",
    Price = 480,
    Description = "Zeus is a loyal and fearless Doberman Pinscher. He is highly trainable and excels in obedience and protection work."
},
new Pet()
{
    Id = 22,
    Name = "Sadie",
    Breed = "Dog - English Bulldog",
    Price = 450,
    Description = "Sadie is a laid-back English Bulldog who loves lounging around the house. She has a playful side and enjoys short walks."
},
new Pet()
{
    Id = 23,
    Name = "Jack",
    Breed = "Dog - Jack Russell Terrier",
    Price = 300,
    Description = "Jack is a spirited Jack Russell Terrier with endless energy. He loves chasing balls and exploring his surroundings."
},
new Pet()
{
    Id = 24,
    Name = "Ruby",
    Breed = "Dog - Pomeranian",
    Price = 350,
    Description = "Ruby is a fluffy and affectionate Pomeranian. She enjoys being the center of attention and will happily perform tricks for treats."
},
new Pet()
{
    Id = 25,
    Name = "Teddy",
    Breed = "Dog - Golden Doodle",
    Price = 500,
    Description = "Teddy is a playful and outgoing Golden Doodle. He is great with kids and loves romping around in the backyard."
},
new Pet()
{
    Id = 26,
    Name = "Maggie",
    Breed = "Dog - Maltese",
    Price = 320,
    Description = "Maggie is a charming Maltese with a gentle disposition. She enjoys snuggling in blankets and going for leisurely walks."
},
new Pet()
{
    Id = 27,
    Name = "Jake",
    Breed = "Dog - Basset Hound",
    Price = 380,
    Description = "Jake is a laid-back Basset Hound with a nose for adventure. He loves following scents and lounging in sunny spots."
},
new Pet()
{
    Id = 28,
    Name = "Roxy",
    Breed = "Dog - Bull Terrier",
    Price = 420,
    Description = "Roxy is a spunky Bull Terrier with a playful personality. She enjoys running around the yard and playing with toys."
},
new Pet()
{
    Id = 29,
    Name = "Gus",
    Breed = "Dog - West Highland White Terrier",
    Price = 380,
    Description = "Gus is a spirited West Highland White Terrier known for his spunky attitude. He loves digging and exploring outdoors."
},
new Pet()
{
    Id = 30,
    Name = "Bailey",
    Breed = "Dog - Boxer",
    Price = 450,
    Description = "Bailey is a friendly and energetic Boxer. He enjoys playing fetch and going for runs with his human companions."
},
new Pet()
{
    Id = 31,
    Name = "Holly",
    Breed = "Dog - Saint Bernard",
    Price = 600,
    Description = "Holly is a gentle giant Saint Bernard known for her loving nature. She enjoys cuddling and being around people."
},
new Pet()
{
    Id = 32,
    Name = "Max",
    Breed = "Dog - Labrador Retriever",
    Price = 350,
    Description = "Max is a loyal and intelligent Labrador Retriever. He excels in obedience training and loves swimming and fetching."
},
new Pet()
{
    Id = 33,
    Name = "Coco",
    Breed = "Dog - French Bulldog",
    Price = 550,
    Description = "Coco is an adorable French Bulldog with a big personality. She loves snuggling and entertaining her family with her antics."
},
new Pet()
{
    Id = 34,
    Name = "Milo",
    Breed = "Dog - Miniature Schnauzer",
    Price = 380,
    Description = "Milo is a playful Miniature Schnauzer with a mischievous streak. He enjoys learning new tricks and going for walks in the park."
},
new Pet()
{
    Id = 35,
    Name = "Lily",
    Breed = "Dog - English Springer Spaniel",
    Price = 400,
    Description = "Lily is a loving and obedient English Springer Spaniel. She enjoys outdoor activities like hiking and playing fetch."
},
new Pet()
{
    Id = 36,
    Name = "Apollo",
    Breed = "Dog - Great Pyrenees",
    Price = 500,
    Description = "Apollo is a majestic Great Pyrenees known for his protective instincts. He loves snowy winters and patrolling his territory."
},
new Pet()
{
    Id = 37,
    Name = "Buddy",
    Breed = "Dog - Golden Retriever",
    Price = 300,
    Description = "Buddy is a friendly and loyal Golden Retriever. He enjoys swimming, fetching, and spending time with his family."
},
new Pet()
{
    Id = 38,
    Name = "Charlie",
    Breed = "Dog - Poodle",
    Price = 200,
    Description = "Charlie is a smart and elegant Poodle with a playful spirit. He enjoys learning new tricks and going for walks in the park."
},
new Pet()
{
    Id = 39,
    Name = "Bailey",
    Breed = "Dog - Australian Cattle Dog",
    Price = 450,
    Description = "Bailey is an intelligent and energetic Australian Cattle Dog. She enjoys agility training and hiking adventures."
},
new Pet()
{
    Id = 40,
    Name = "Duke",
    Breed = "Dog - Weimaraner",
    Price = 480,
    Description = "Duke is a noble Weimaraner known for his grace and athleticism. He enjoys outdoor activities and bonding with his family."
},
new Pet()
{
    Id = 41,
    Name = "Mia",
    Breed = "Dog - Bichon Frise",
    Price = 320,
    Description = "Mia is a cheerful Bichon Frise with a love for life. She enjoys playing games and spending time with her human companions."
},
new Pet()
{
    Id = 42,
    Name = "Rocky",
    Breed = "Dog - German Shepherd",
    Price = 400,
    Description = "Rocky is a courageous and loyal German Shepherd. He excels in obedience training and enjoys being challenged mentally and physically."
},
new Pet()
{
    Id = 43,
    Name = "Princess",
    Breed = "Dog - American Eskimo Dog",
    Price = 380,
    Description = "Princess is a playful American Eskimo Dog with a fluffy white coat. She enjoys learning tricks and going for walks in the snow."
},
new Pet()
{
    Id = 44,
    Name = "Sasha",
    Breed = "Dog - Alaskan Malamute",
    Price = 550,
    Description = "Sasha is a strong and independent Alaskan Malamute. She loves cold weather and enjoys pulling sleds and carts."
},
new Pet()
{
    Id = 45,
    Name = "Shadow",
    Breed = "Dog - Border Terrier",
    Price = 380,
    Description = "Shadow is a brave and adventurous Border Terrier. He enjoys exploring the great outdoors and chasing small animals."
},
new Pet()
{
    Id = 46,
    Name = "Milo",
    Breed = "Dog - Golden Retriever",
    Price = 350,
    Description = "Milo is a lovable Golden Retriever with a heart of gold. He enjoys playing fetch and swimming in the lake."
},
new Pet()
{
    Id = 47,
    Name = "Luna",
    Breed = "Dog - Siberian Husky",
    Price = 500,
    Description = "Luna is an energetic Siberian Husky with striking blue eyes. She loves running and playing in the snow."
},
new Pet()
{
    Id = 48,
    Name = "Dexter",
    Breed = "Dog - Staffordshire Bull Terrier",
    Price = 420,
    Description = "Dexter is a strong and determined Staffordshire Bull Terrier. He enjoys training sessions and playing with his favorite toys."
},
new Pet()
{
    Id = 49,
    Name = "Ruby",
    Breed = "Dog - Yorkshire Terrier",
    Price = 300,
    Description = "Ruby is a sassy Yorkshire Terrier with a big personality. She loves being the center of attention and going for walks."
},
new Pet()
{
    Id = 50,
    Name = "Teddy",
    Breed = "Dog - Shih Tzu",
    Price = 300,
    Description = "Teddy is a fluffy Shih Tzu with a sweet disposition. He enjoys cuddling on the couch and getting pampered with grooming sessions."
}

            };
            return pets;
        }

    }
}
