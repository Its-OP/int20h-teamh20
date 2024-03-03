using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Authorize]
[Route("api/messages")]
public class MessageController(IApplicationDbContext context) : ControllerBase
{
    private readonly IApplicationDbContext _context = context;

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetMessages(CancellationToken token,
        [FromQuery] int messagesCount = 5)
    {
        if (messagesCount < 1)
            return BadRequest("Messages count must be greater than 1.");

        messagesCount = Math.Min(messagesCount, 15);
        var messages = await GetMessagesOfStudent(token);
        return Ok(messages.OrderByDescending(x => x.IsRead).ThenByDescending(x => x.CreatedAt)
                .Take(messagesCount).Select(x => new MessageContract(x)).ToList());
    }

    [HttpGet]
    [Route("{messageId:int}")]
    public async Task<IActionResult> GetMessage([FromRoute] int messageId, CancellationToken token)
    {
        return Ok(new MessageContract(await GetMessageById(messageId, token)));
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateMessage([FromBody] MessageParams apiMessage, CancellationToken token)
    {
        var receiverIds = apiMessage.ReceiverIds.Distinct().ToList();
        var receivers = await _context.Students.Where(x => receiverIds.Contains(x.Id)).ToListAsync(token);

        var messages = new List<NotificationMessage>();
        foreach (var receiver in receivers)
        {
            var message = new NotificationMessage
            {
                Title = apiMessage.Title,
                Text = apiMessage.Text,
                Receiver = receiver,
            };

            messages.Add(message);
        }

        await _context.Messages.AddRangeAsync(messages, token);
        return await ResponseOnSaveAsync(token);
    }

    [HttpPost]
    [Route("read/{messageId:int}")]
    public async Task<IActionResult> ReadMessage([FromRoute] int messageId, CancellationToken token)
    {
        var message = await GetMessageById(messageId, token);
        message.IsRead = true;
        return await ResponseOnSaveAsync(token);
    }

    private async Task<IActionResult> ResponseOnSaveAsync(CancellationToken token)
    {
        int count = await _context.SaveChangesAsync(token);
        return Ok(new { Result = count != 0 });
    }

    private async Task<IEnumerable<NotificationMessage>> GetMessagesOfStudent(CancellationToken token)
    {
        var userId = Helpers.GetUserId(User);
        var student = await _context.Students.SingleOrDefaultAsync(x => x.User.Id == userId, token)
                    ?? throw new BadRequestException("Invalid student id.");
        return student.Messages;
    }

    private async Task<NotificationMessage> GetMessageById(int id, CancellationToken token)
    {
        var messages = await GetMessagesOfStudent(token);
        return messages.FirstOrDefault(x => x.Id == id)
                ?? throw new BadRequestException("Invalid message id.");
    }
}