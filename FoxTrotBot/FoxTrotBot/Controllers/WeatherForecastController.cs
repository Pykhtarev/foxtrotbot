using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FoxTrotBot.Controllers
{
    
        [Serializable]
        public class Updates
        {
            /// <summary>
            /// Тип события
            /// </summary>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Объект, инициировавший событие
            /// Структура объекта зависит от типа уведомления
            /// </summary>
            [JsonProperty("object")]
            public JObject Object { get; set; }

            /// <summary>
            /// ID сообщества, в котором произошло событие
            /// </summary>
            [JsonProperty("group_id")]
            public long GroupId { get; set; }
        }
    
}
