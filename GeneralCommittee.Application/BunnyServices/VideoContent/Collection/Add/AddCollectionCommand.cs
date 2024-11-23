using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Collection.Add
{
    public class AddCollectionCommand : IRequest<string>
    {
        public string CollectionName { get; set; } = default!;
    }
}
