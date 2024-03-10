using Domain.Abstraction;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatGroupRepository _chatGroupRepository;
        private readonly MessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public ChatHub(ChatGroupRepository chatGroupRepository, MessageRepository messageRepository, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _chatGroupRepository = chatGroupRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }
        //{"protocol":"json","version":1}
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("From hubs !");
            await Clients.All.SendAsync("ReceiveMessage ", $"{Context.ConnectionId} has joinn ");
        }
        //{ "arguments": [1, 1], "target": "CreateGroupAndAddUsers", "type": 1}
        public async Task CreateGroupAndAddUsers(int customerId, int employeeId)
        {
            Console.WriteLine("From hubs !");
            string customerUserId = null;
            string employeeUserId = null;
            var customer = await _unitOfWork.Clients.GetById(customerId);
            if (customer != null)
            {
                var customerUser = await _userManager.FindByEmailAsync(customer.Email);
                if (customerUser != null)
                {
                    customerUserId = customerUser.Id;
                    Console.WriteLine("Customer Id : " + customerUserId);
                }
            }
            var employee = await _unitOfWork.Employees.GetById(employeeId);
            if (employee != null)
            {
                var employeeUser = await _userManager.FindByEmailAsync(employee.Email);
                if (employeeUser != null)
                {
                    employeeUserId = employeeUser.Id;
                    Console.WriteLine("Employee Id : " + employeeUserId);
                }
            }

            string groupName = $"{customer.ClientId}-{employee.EmployeeId}";
            Console.WriteLine(groupName);
            // Tạo một đối tượng ChatGroup mới
            if (customerUserId != null && employeeUserId != null)
            {

                var newGroup = new ChatGroup
                {
                    Name = groupName,
                    ClientId = customerUserId,
                    EmployeeId = employeeUserId
                };
                Console.WriteLine("Creating ....");
                await _chatGroupRepository.InsertChatGroupAsync(newGroup);
                Console.WriteLine("Done !");
                // Thêm cả hai người dùng vào nhóm
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.Group(groupName).SendAsync("ReceiveMessage", $"User has joinn ");
            }
        }
        //{ "arguments": ["1-2", "Minh" , "Test Message"], "target": "SendMessageToGroup", "type": 1}
        public async Task SendMessageToGroup(string groupName, string senderId, string reciverId, string message)
        {
            var groupId = _chatGroupRepository.GetIdByName(groupName);

            Console.WriteLine("Send Message");
            var newMessage = new Message()
            {
                Content = message,
                GroupId = groupId,
                SenderId = senderId,
                ReceiveId = reciverId,
            };
            var sender = await _userManager.FindByIdAsync(senderId);
            if (sender == null)
            {
                Console.WriteLine("Sender null!");
            }
            string senderName = sender.UserName;
            await _messageRepository.InsertMessageAsync(newMessage);
            await Clients.Group(groupName).SendAsync("ReceiveMessage", senderName, message);
        }
        //{ "arguments": [1,2], "target": "GetGroupMessage", "type": 1}
        public async Task GetGroupMessage(string groupName)
        {
            try
            {
                var groupId = _chatGroupRepository.GetIdByName(groupName);
                // Lấy tất cả các tin nhắn trong nhóm dựa trên tên nhóm
                Console.WriteLine("Getting message from repo ....");
                var messages = await _messageRepository.GetAllByGroupId(groupId);
                // Gửi các tin nhắn đến client
                Console.WriteLine("Finding sender ....");
                foreach (var message in messages)
                {
                    var sender = await _userManager.FindByIdAsync(message.SenderId);
                    if (sender != null)
                    {
                        string senderName = sender.UserName;
                        await Clients.Caller.SendAsync("ReceiveMessage", senderName, message.Content);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving group messages: {ex.Message}");
            }
        }
        public async Task GetAllGroupByEmployeeId(int employeeId)
        {
            try
            {
                var groups = await _chatGroupRepository.GetGroupByEmployeeId(employeeId);

                foreach (var group in groups)
                {
                    if (group != null)
                    {
                        var groupName = group.Name;
                        await Clients.Caller.SendAsync("ReceiveMessage", groupName);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while retrieving group messages: {ex.Message}");
            }
        }
    }
}
