using System.Threading.Tasks;
using Examination.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Examination.API.Controllers.V2;

[ApiController]
[ApiVersion( "2.0" )]
[Route( "api/v{version:apiVersion}/[controller]" )]
public class ExamController :  ControllerBase
{
    private readonly IMediator _mediator;

    public ExamController(IMediator mediator)
    { 
        _mediator = mediator;
    }
        
    [HttpGet]
    public async Task<IActionResult> GetExamList(string parameter)
    {
        var exams= await _mediator.Send(new GetHomeExamListQuery() { });
        return Ok(exams);
    }
}