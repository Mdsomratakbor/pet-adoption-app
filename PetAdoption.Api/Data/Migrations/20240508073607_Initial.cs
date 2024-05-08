using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetAdoption.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false),
                    AdoptionStatus = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAdoption",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    AdoptedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdoption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdoption_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAdoption_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFavorites",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorites", x => new { x.UserId, x.PetId });
                    table.ForeignKey(
                        name: "FK_UserFavorites_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "AdoptionStatus", "Breed", "DateOfBirth", "Description", "Gender", "Image", "IsActive", "Name", "Price", "Views" },
                values: new object[,]
                {
                    { 1, 1, "Dog - Golden Retriever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buddy is a loyal and friendly Golden Retriever. He enjoys playing fetch, going for long walks, and cuddling with his family.", 0, null, true, "Buddy", 300.0, 0 },
                    { 2, 1, "Cat - Siamese", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whiskers is a sleek and elegant Siamese cat. He enjoys lounging in sunny spots, chasing toys, and receiving affection from his human companions.", 0, null, true, "Whiskers", 150.0, 0 },
                    { 3, 1, "Dog - German Shepherd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rocky is a brave and intelligent German Shepherd. He is highly trainable, loyal, and protective of his family. Rocky enjoys obedience training, playing games, and going on outdoor adventures.", 0, null, true, "Rocky", 400.0, 0 },
                    { 4, 1, "Dog - Labrador Retriever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max is a friendly Labrador Retriever with a love for fetching. He enjoys spending time outdoors and playing with his favorite toys.", 0, null, true, "Max", 350.0, 0 },
                    { 5, 1, "Dog - Beagle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Daisy is a curious Beagle with a nose for adventure. She loves exploring new scents and going on long walks with her family.", 0, null, true, "Daisy", 250.0, 0 },
                    { 6, 1, "Dog - Siberian Husky", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luna is an energetic Siberian Husky who thrives in cold climates. She enjoys running, hiking, and pulling sleds in the snow.", 0, null, true, "Luna", 500.0, 0 },
                    { 7, 1, "Dog - Poodle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie is an elegant Poodle known for his intelligence and grace. He enjoys learning new tricks and showing off his skills.", 0, null, true, "Charlie", 200.0, 0 },
                    { 8, 1, "Dog - Boxer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bailey is a playful Boxer with a heart of gold. He loves romping around in the yard and chasing after balls with his boundless energy.", 0, null, true, "Bailey", 450.0, 0 },
                    { 9, 1, "Dog - Shih Tzu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bella is a charming Shih Tzu known for her affectionate nature. She enjoys cuddling on the couch and being pampered with grooming sessions.", 0, null, true, "Bella", 300.0, 0 },
                    { 10, 1, "Dog - Border Collie", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cooper is a friendly and intelligent Border Collie. He loves playing fetch and going for long walks.", 0, null, true, "Cooper", 380.0, 0 },
                    { 11, 1, "Dog - Dachshund", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lucy is a playful Dachshund with a big heart. She enjoys cuddling on the couch and exploring new places.", 0, null, true, "Lucy", 280.0, 0 },
                    { 12, 1, "Dog - Bulldog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Toby is a loyal Bulldog who adores his family. He enjoys napping in the sun and chewing on his favorite toys.", 0, null, true, "Toby", 420.0, 0 },
                    { 13, 1, "Dog - Cocker Spaniel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Molly is a gentle Cocker Spaniel who loves being pampered. She enjoys baths, brushing, and snuggling up with her humans.", 0, null, true, "Molly", 320.0, 0 },
                    { 14, 1, "Dog - Rottweiler", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Oscar is a strong and protective Rottweiler. He is great with kids and loves going on adventures in the great outdoors.", 0, null, true, "Oscar", 450.0, 0 },
                    { 15, 1, "Dog - Shetland Sheepdog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zoe is a clever Shetland Sheepdog who excels in agility training. She is always up for a challenge and loves learning new tricks.", 0, null, true, "Zoe", 350.0, 0 },
                    { 16, 1, "Dog - Australian Shepherd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sam is an energetic Australian Shepherd who thrives on mental and physical stimulation. He enjoys herding and playing frisbee.", 0, null, true, "Sam", 500.0, 0 },
                    { 17, 1, "Dog - Great Dane", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Riley is a majestic Great Dane with a gentle demeanor. Despite his size, he is a big softie who loves cuddling on the couch.", 0, null, true, "Riley", 600.0, 0 },
                    { 18, 1, "Dog - Pembroke Welsh Corgi", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lola is a charismatic Pembroke Welsh Corgi who charms everyone she meets. She enjoys herding and participating in dog sports.", 0, null, true, "Lola", 380.0, 0 },
                    { 19, 1, "Dog - Bernese Mountain Dog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bentley is a friendly and affectionate Bernese Mountain Dog. He loves snowy days and pulling carts or sleds.", 0, null, true, "Bentley", 550.0, 0 },
                    { 20, 1, "Dog - Cavalier King Charles Spaniel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sophie is a sweet and gentle Cavalier King Charles Spaniel. She adores cuddling and will follow her humans everywhere.", 0, null, true, "Sophie", 400.0, 0 },
                    { 21, 1, "Dog - Doberman Pinscher", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zeus is a loyal and fearless Doberman Pinscher. He is highly trainable and excels in obedience and protection work.", 0, null, true, "Zeus", 480.0, 0 },
                    { 22, 1, "Dog - English Bulldog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sadie is a laid-back English Bulldog who loves lounging around the house. She has a playful side and enjoys short walks.", 0, null, true, "Sadie", 450.0, 0 },
                    { 23, 1, "Dog - Jack Russell Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jack is a spirited Jack Russell Terrier with endless energy. He loves chasing balls and exploring his surroundings.", 0, null, true, "Jack", 300.0, 0 },
                    { 24, 1, "Dog - Pomeranian", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ruby is a fluffy and affectionate Pomeranian. She enjoys being the center of attention and will happily perform tricks for treats.", 0, null, true, "Ruby", 350.0, 0 },
                    { 25, 1, "Dog - Golden Doodle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teddy is a playful and outgoing Golden Doodle. He is great with kids and loves romping around in the backyard.", 0, null, true, "Teddy", 500.0, 0 },
                    { 26, 1, "Dog - Maltese", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Maggie is a charming Maltese with a gentle disposition. She enjoys snuggling in blankets and going for leisurely walks.", 0, null, true, "Maggie", 320.0, 0 },
                    { 27, 1, "Dog - Basset Hound", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jake is a laid-back Basset Hound with a nose for adventure. He loves following scents and lounging in sunny spots.", 0, null, true, "Jake", 380.0, 0 },
                    { 28, 1, "Dog - Bull Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Roxy is a spunky Bull Terrier with a playful personality. She enjoys running around the yard and playing with toys.", 0, null, true, "Roxy", 420.0, 0 },
                    { 29, 1, "Dog - West Highland White Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gus is a spirited West Highland White Terrier known for his spunky attitude. He loves digging and exploring outdoors.", 0, null, true, "Gus", 380.0, 0 },
                    { 30, 1, "Dog - Boxer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bailey is a friendly and energetic Boxer. He enjoys playing fetch and going for runs with his human companions.", 0, null, true, "Bailey", 450.0, 0 },
                    { 31, 1, "Dog - Saint Bernard", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Holly is a gentle giant Saint Bernard known for her loving nature. She enjoys cuddling and being around people.", 0, null, true, "Holly", 600.0, 0 },
                    { 32, 1, "Dog - Labrador Retriever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max is a loyal and intelligent Labrador Retriever. He excels in obedience training and loves swimming and fetching.", 0, null, true, "Max", 350.0, 0 },
                    { 33, 1, "Dog - French Bulldog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Coco is an adorable French Bulldog with a big personality. She loves snuggling and entertaining her family with her antics.", 0, null, true, "Coco", 550.0, 0 },
                    { 34, 1, "Dog - Miniature Schnauzer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milo is a playful Miniature Schnauzer with a mischievous streak. He enjoys learning new tricks and going for walks in the park.", 0, null, true, "Milo", 380.0, 0 },
                    { 35, 1, "Dog - English Springer Spaniel", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lily is a loving and obedient English Springer Spaniel. She enjoys outdoor activities like hiking and playing fetch.", 0, null, true, "Lily", 400.0, 0 },
                    { 36, 1, "Dog - Great Pyrenees", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apollo is a majestic Great Pyrenees known for his protective instincts. He loves snowy winters and patrolling his territory.", 0, null, true, "Apollo", 500.0, 0 },
                    { 37, 1, "Dog - Golden Retriever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Buddy is a friendly and loyal Golden Retriever. He enjoys swimming, fetching, and spending time with his family.", 0, null, true, "Buddy", 300.0, 0 },
                    { 38, 1, "Dog - Poodle", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie is a smart and elegant Poodle with a playful spirit. He enjoys learning new tricks and going for walks in the park.", 0, null, true, "Charlie", 200.0, 0 },
                    { 39, 1, "Dog - Australian Cattle Dog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bailey is an intelligent and energetic Australian Cattle Dog. She enjoys agility training and hiking adventures.", 0, null, true, "Bailey", 450.0, 0 },
                    { 40, 1, "Dog - Weimaraner", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Duke is a noble Weimaraner known for his grace and athleticism. He enjoys outdoor activities and bonding with his family.", 0, null, true, "Duke", 480.0, 0 },
                    { 41, 1, "Dog - Bichon Frise", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mia is a cheerful Bichon Frise with a love for life. She enjoys playing games and spending time with her human companions.", 0, null, true, "Mia", 320.0, 0 },
                    { 42, 1, "Dog - German Shepherd", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rocky is a courageous and loyal German Shepherd. He excels in obedience training and enjoys being challenged mentally and physically.", 0, null, true, "Rocky", 400.0, 0 },
                    { 43, 1, "Dog - American Eskimo Dog", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Princess is a playful American Eskimo Dog with a fluffy white coat. She enjoys learning tricks and going for walks in the snow.", 0, null, true, "Princess", 380.0, 0 },
                    { 44, 1, "Dog - Alaskan Malamute", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sasha is a strong and independent Alaskan Malamute. She loves cold weather and enjoys pulling sleds and carts.", 0, null, true, "Sasha", 550.0, 0 },
                    { 45, 1, "Dog - Border Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shadow is a brave and adventurous Border Terrier. He enjoys exploring the great outdoors and chasing small animals.", 0, null, true, "Shadow", 380.0, 0 },
                    { 46, 1, "Dog - Golden Retriever", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Milo is a lovable Golden Retriever with a heart of gold. He enjoys playing fetch and swimming in the lake.", 0, null, true, "Milo", 350.0, 0 },
                    { 47, 1, "Dog - Siberian Husky", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Luna is an energetic Siberian Husky with striking blue eyes. She loves running and playing in the snow.", 0, null, true, "Luna", 500.0, 0 },
                    { 48, 1, "Dog - Staffordshire Bull Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dexter is a strong and determined Staffordshire Bull Terrier. He enjoys training sessions and playing with his favorite toys.", 0, null, true, "Dexter", 420.0, 0 },
                    { 49, 1, "Dog - Yorkshire Terrier", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ruby is a sassy Yorkshire Terrier with a big personality. She loves being the center of attention and going for walks.", 0, null, true, "Ruby", 300.0, 0 },
                    { 50, 1, "Dog - Shih Tzu", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teddy is a fluffy Shih Tzu with a sweet disposition. He enjoys cuddling on the couch and getting pampered with grooming sessions.", 0, null, true, "Teddy", 300.0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoption_PetId",
                table: "UserAdoption",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdoption_UserId",
                table: "UserAdoption",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorites_PetId",
                table: "UserFavorites",
                column: "PetId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAdoption");

            migrationBuilder.DropTable(
                name: "UserFavorites");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
