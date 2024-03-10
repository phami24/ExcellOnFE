using API.Hubs;
using Domain.Abstraction;
using Domain.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ChatGroupRepository _chatGroupRepository;
        private readonly MessageRepository _messageRepository;
        private readonly IHubContext<ChatHub> _hub;
        private readonly IUnitOfWork _unitOfWork;
        public ChatController(
            ChatGroupRepository chatGroupRepository,
            MessageRepository messageRepository,
            IHubContext<ChatHub> hub,
            IUnitOfWork unitOfWork
            )
        {
            _chatGroupRepository = chatGroupRepository;
            _messageRepository = messageRepository;
            _hub = hub;
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGroupByEmployeeId(int employeeId)
        {
            var groups = await _chatGroupRepository.GetGroupByEmployeeId(employeeId);
            if (groups == null)
            {
                return BadRequest("No groups has found !");
            }
            return Ok(groups);
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroupAndAddUser(int employeeId, int customerId)
        {
            var newGroupName = $"{employeeId}-{customerId}";

            var existingGroup = _chatGroupRepository.GetIdByName(newGroupName);
            if (existingGroup != null)
            {
                return BadRequest("Group already exists");
            }

            var newGroup = new ChatGroup()
            {
                ClientId = customerId,
                EmployeeId = employeeId,
                Name = newGroupName,
            };

            var isCreated = _chatGroupRepository.InsertChatGroupAsync(newGroup);
            if (isCreated.IsCompleted)
            {
                return Ok("Group has been created successfully!");
            }

            return BadRequest("Failed to create the group");
        }

        [HttpPost]
        [Route("JoinGroupChat")]
        public async Task<IActionResult> JoinGroupChat(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return BadRequest("Group name can't null or empty !");
            }
            await _hub.Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
