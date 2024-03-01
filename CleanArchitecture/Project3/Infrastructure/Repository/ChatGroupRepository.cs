using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Config;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repository
{
    public class ChatGroupRepository : IChatGroupRepository
    {
        private readonly IMongoCollection<ChatGroup> _chatGroupCollection;

        public ChatGroupRepository(IOptions<ChatDatabaseSettings> chatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            chatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                chatDatabaseSettings.Value.DatabaseName);
            _chatGroupCollection = mongoDatabase.GetCollection<ChatGroup>("ChatGroups");
        }

        public async Task<IEnumerable<ChatGroup>> All()
        {
            try
            {
                var chatGroups = await _chatGroupCollection.Find(_ => true).ToListAsync();
                return chatGroups;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving chat groups: {ex.Message}");
                return null;
            }
        }

        public string GetIdByName(string groupName)
        {
            try
            {
                var group = _chatGroupCollection.Find(group => group.Name == groupName).FirstOrDefault();
                if (group == null)
                {
                    Console.WriteLine($"{groupName} is not a chat group");
                }
                return group.Id.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving chat group ID: {ex.Message}");
                return null;
            }
        }


        public async Task InsertChatGroupAsync(ChatGroup chatGroup)
        {
            try
            {
                var existingGroup = await _chatGroupCollection.Find(group => group.Name == chatGroup.Name).FirstOrDefaultAsync();
                Console.WriteLine("Chat Repo ");

                if (existingGroup == null)
                {
                    await _chatGroupCollection.InsertOneAsync(chatGroup);
                }
                else
                {
                    Console.WriteLine("Group already exists.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while inserting chat group: {ex.Message}");
            }
        }

    }
}
