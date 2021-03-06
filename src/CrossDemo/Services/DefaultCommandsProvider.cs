﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MatrixContent.Framework;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace CrossDemo.Services
{
    public class DefaultCommandsProvider:IApiCommandProvider
    {
        readonly IServiceProvider mServiceProvider;

        public DefaultCommandsProvider(IServiceProvider serviceProvider)
        {
            mServiceProvider = serviceProvider;
        }
        /// <summary>
        /// Gets the commands.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string,Func<object,object>> GetCommands()
        {
            var commands = new Dictionary<string,Func<object,object>>();

            foreach(var item in this.DiscoveryApiCommands())
            {
                commands.Add(item.Key,item.Value);
            }
            return commands;
        }

        /// <summary>
        /// Invalids the response.
        /// </summary>
        /// <returns></returns>
        object InvalidResponse()
        {
            return new
            {
                Success = false,
                Message = "Operation failed."
            };
        }
        /// <summary>
        /// Gets the instruments.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [ApiCommand("GetInstruments")]
        object GetInstruments(dynamic parameters)
        {
            var env = mServiceProvider.GetService<IHostingEnvironment>();
            var file = Path.Combine(env.WebRootPath,"data","Instruments.json");

            if(File.Exists(file))
            {
                var json = File.ReadAllText(file);
                return JsonConvert.DeserializeObject(json);
            }
            else
            {
                return new {Success=false,Message="File does not exist"};
            }
        }
        /// <summary>
        /// Gets the instrument.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [ApiCommand("GetInstrument")]
        object GetInstrument(dynamic parameters)
        {
            var env = mServiceProvider.GetService<IHostingEnvironment>();
            var file = Path.Combine(env.WebRootPath,"data","Instruments.json");
            int id = parameters.Id;

            if(File.Exists(file))
            {
                var json = File.ReadAllText(file);
                var instruments = JsonConvert.DeserializeObject<IEnumerable<InstrumentModel>>(json);

                return instruments.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                return InvalidResponse();
            }
        }
    }
}
