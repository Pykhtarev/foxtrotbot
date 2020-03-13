using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using VkNet.Abstractions;
using VkNet.Model;
using VkNet.Model.RequestParams;
using VkNet.Utils;

namespace FoxTrotBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly IVkApi _vkApi;

        public CallbackController(IVkApi vkApi, IConfiguration configuration)
        {
            _vkApi = vkApi;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            // Проверяем, что находится в поле "type" 
            switch (updates.Type)
            {
                // Если это уведомление для подтверждения адреса
                case "confirmation":
                    // Отправляем строку для подтверждения 
                    return Ok(_configuration["Config:Confirmation"]);
                case "message_new":
                {
                    // Десериализация
                    var msg = Message.FromJson(new VkResponse(updates.Object));

                    // Отправим в ответ полученный от пользователя текст
                    _vkApi.Messages.Send(new MessagesSendParams
                    {
                        RandomId = new DateTime(125446).Millisecond,
                        PeerId = msg.UserId.Value,
                        Message = msg.Text
                    });
                    break;
                }
            }

            // Возвращаем "ok" серверу Callback API
            return Ok("ok");
        }
    }
}