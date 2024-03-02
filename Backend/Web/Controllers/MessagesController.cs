using backend.ApiContracts;
using domain;
using domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[Route("api/messages")]
public class MessagesController(IApplicationDbContext context) : ControllerBase
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
        return Ok(messages.OrderByDescending(x => x.Id).Take(messagesCount)
                .Select(x => new MessageContract(x)).ToList());
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
        // TODO: template logic

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
        int count = await _context.SaveChangesAsync(token);

        return Ok(new { Result = count != 0 });
    }

    [HttpPost]
    [Route("read/{messageId:int}")]
    public async Task<IActionResult> ReadMessage([FromRoute] int messageId, CancellationToken token)
    {
        var message = await GetMessageById(messageId, token);
        message.IsRead = true;
        var count = await _context.SaveChangesAsync(token);

        return Ok(new { Result = count != 0 });
    }

    private async Task<IEnumerable<NotificationMessage>> GetMessagesOfStudent(CancellationToken token)
    {
        var id = User.GetUserID();
        var student = await _context.Students.SingleOrDefaultAsync(x => x.Id == id, token)
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