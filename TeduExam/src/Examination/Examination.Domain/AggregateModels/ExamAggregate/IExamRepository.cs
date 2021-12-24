using System.Collections.Generic;
using System.Threading.Tasks;
using Examination.Domain.SeedWork;

namespace Examination.Domain.AggregateModels.ExamAggregate
{
    public interface IExamRepository  //: IRepositoryBase<Exam>  
    {
        public Task<IEnumerable<Exam>> GetExamListAsync();
        public Task<Exam> GetExamByIdAsync(string id);
    }
}