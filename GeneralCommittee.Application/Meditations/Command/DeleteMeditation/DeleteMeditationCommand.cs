using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Command.DeleteMeditation
{
    public class DeleteMeditationCommand : IRequest<Unit>
    {public int MeditationId { get; set; }
     public string Title { get; set; }}}
