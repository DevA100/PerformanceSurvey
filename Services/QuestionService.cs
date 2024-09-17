using PerformanceSurvey.iRepository;
using PerformanceSurvey.iServices;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Repository;
using PerformanceSurvey.Models.RequestDTOs.ResponseDTOs;
using PerformanceSurvey.Models.RequestDTOs;

namespace PerformanceSurvey.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public async Task<MultipleChoiceQuestionResponse> CreateDepartmentMultipleQuestionAsync(MultipleChoiceQuestionRequest questionDto)
        {
            // Map the request DTO to the entity model 'Question'
            var question = new Question
            {
                QuestionText = questionDto.QuestionText,
                DepartmentId = questionDto.DepartmentId,
                CreatedAt = DateTime.UtcNow,
                Options = questionDto.Options?.Select(o => new QuestionOption // Assuming 'Option' is the entity model
                {
                    Text = o.Text,
                    Score = o.Score ?? 0 // Default value for Score
                }).ToList()
            };

            // Call the repository to save the question (this expects a Question entity, not a DTO)
            await _repository.AddDepartmentMultiplechoiceQuestionAsync(question);

            // Map the saved Question entity back to a response DTO
            var response = new MultipleChoiceQuestionResponse
            {
                QuestionText = question.QuestionText,
                Options = question.Options.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    Score = o.Score
                }).ToList()
            };

            return response; // Return the response DTO
        }

        public async Task<TextQuestionResponse> CreateDepartmentTextQuestionAsync(TextQuestionReqest questionDto)
        {
            // Map the request DTO to the Question entity
            var question = new Question
            {
                QuestionText = questionDto.QuestionText,
                DepartmentId = questionDto.DepartmentId,
                CreatedAt = DateTime.UtcNow
            };

            // Call the repository to add the question
            await _repository.AddDepartmentTextQuestionAsync(question);

            // Map the saved Question entity back to the response DTO
            var response = new TextQuestionResponse
            {
                QuestionText = question.QuestionText,
            };

            return response; // Return the response DTO
        }


        public async Task<QuestionDto> GetDepartmentQuestionAsync(int id)
        {
            // Fetch the Question entity from the repository
            var question = await _repository.GetDepartmentQuestionAsync(id);

            // Check if the question was not found
            if (question == null)
            {
                return null; // Or handle this case as needed (e.g., throw an exception or return a specific response)
            }

            // Map Question to QuestionDto
            var questionDto = new QuestionDto
            {
                QuestionText = question.QuestionText,
                Options = question.Options?.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    Score = o.Score 
                }).ToList()
            };

            return questionDto;
        }


        public async Task<IEnumerable<QuestionDto>> GetAllDepartmentQuestionsAsync()
        {
            var questions = await _repository.GetAllDepartmentQuestionsAsync();

            // Map IEnumerable<Question> to IEnumerable<QuestionDto>
            return questions.Select(q => new QuestionDto
            {
                QuestionText = q.QuestionText,
                Options = q.Options?.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    //Score = o.Score
                }).ToList()
            }).ToList();
        }

        public async Task<MultipleChoiceQuestionResponse> UpdateDepartmentMultipleChoiceQuestionAsync(int id, MultipleChoiceQuestionRequest questionDto)
        {
            // Fetch the existing question from the repository
            var question = await _repository.GetDepartmentQuestionAsync(id);

            // Check if the question was not found
            if (question == null)
            {
                return null; // Or handle this case as needed
            }

            // Update the question details
            question.QuestionText = questionDto.QuestionText;
            question.DepartmentId = questionDto.DepartmentId;

            // Process existing options: update or delete as needed
            foreach (var option in question.Options.ToList())
            {
                if (!questionDto.Options.Any(o => o.OptionId == option.OptionId))
                {
                    // Option is no longer in the request, so delete it
                    await _repository.DeleteDepartmentQuestionAsync(option.OptionId); // Make sure this is awaited
                }
                else
                {
                    // Option exists in the request, update it
                    var updatedOption = questionDto.Options.First(o => o.OptionId == option.OptionId);
                    option.Text = updatedOption.Text;
                    option.Score = updatedOption.Score ?? 0;
                }
            }

            // Add new options
            foreach (var newOptionDto in questionDto.Options.Where(o => o.OptionId == 0))
            {
                question.Options.Add(new QuestionOption
                {
                    Text = newOptionDto.Text,
                    Score = newOptionDto.Score ?? 0,
                    QuestionId = id
                });
            }

            // Update the question in the repository
            await _repository.UpdateDepartmentMultipleChoiceQuestionAsync(question);

            // Map the updated question to the response DTO
            var response = new MultipleChoiceQuestionResponse
            {
                QuestionText = question.QuestionText,
                Options = question.Options.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    Score = o.Score
                }).ToList()
            };

            return response;
        }

        public async Task<QuestionOptionDto> GetOptionByIdAsync(int optionId)
        {
            var option = await _repository.GetOptionByIdAsync(optionId);

            // Check if the option is null to handle non-existent cases
            if (option == null)
            {
                return null; // Or handle as per your application's logic, e.g., throw an exception
            }

            // Manually map the entity to DTO
            return new QuestionOptionDto
            {
                OptionId = option.OptionId,
                Text = option.Text,
                Score = option.Score
            };
        }

        public async Task<IEnumerable<QuestionOptionDto>> GetAllOptionsAsync()
        {
            var options = await _repository.GetAllOptionsAsync();

            // Manually map the list of entities to a list of DTOs
            return options.Select(option => new QuestionOptionDto
            {
                OptionId = option.OptionId,
                Text = option.Text,
                Score = option.Score
            }).ToList();
        }


        public async Task<TextQuestionResponse> UpdateDepartmentTextQuestionAsync(int id, TextQuestionReqest questionDto)
        {
            // Fetch the existing question from the repository
            var question = await _repository.GetDepartmentQuestionAsync(id);

            // Check if the question was not found
            if (question == null)
            {
                return null; // Or handle this case as needed (e.g., throw an exception or return a specific response)
            }

            // Update the question details
            question.QuestionText = questionDto.QuestionText;
            question.DepartmentId = questionDto.DepartmentId;

            // Save the updated question in the repository
            await _repository.UpdateDepartmentTextQuestionAsync(question);

            // Map the updated question to TextQuestionResponse
            var response = new TextQuestionResponse
            {
                QuestionText = question.QuestionText,
            };

            return response;
        }


        public async Task<Question> DeleteDepartmentQuestionAsync(int id)
        {
            var question = await _repository.GetDepartmentQuestionAsync(id);
            if (question != null)
            {
                await _repository.DeleteDepartmentQuestionAsync(id);
            }
            return question;
        }

        public async Task<IEnumerable<GetQuestionByDepartmentDto>> GetDepartmentQuestionsByDepartmentIdAsync(int departmentId)
        {
            var questions = await _repository.GetDepartmentQuestionsByDepartmentIdAsync(departmentId);
            return questions.Select(q => new GetQuestionByDepartmentDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                Options = q.Options?.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    //Score = o.Score
                }).ToList()
            }) .ToList();
        }

        public async Task<IEnumerable<GetQuestionByDepartmentDto>> GetDepartmentQuestionsByDepartmentIdsAsync(IEnumerable<int> departmentIds)
        {
            // Fetch the questions from the repository
            var questions = await _repository.GetDepartmentQuestionsByDepartmentIdsAsync(departmentIds);

            // Map the Question entities to GetQuestionByDepartmentDto
            var questionDtos = questions.Select(q => new GetQuestionByDepartmentDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                // Map other properties as needed
                Options = q.Options?.Select(o => new QuestionOptionDto
                {
                    Text = o.Text,
                    // Map other properties if necessary
                }).ToList()
            });

            return questionDtos;
        }

    }

}
