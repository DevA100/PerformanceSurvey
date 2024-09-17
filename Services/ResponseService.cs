using PerformanceSurvey.iRepository;
using PerformanceSurvey.iServices;
using PerformanceSurvey.Models.DTOs;
using PerformanceSurvey.Models;
using Microsoft.EntityFrameworkCore;
using PerformanceSurvey.Repository;
using OfficeOpenXml;

namespace PerformanceSurvey.Services
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _responseRepository;
        private readonly IQuestionRepository _questionRepository;
        public ResponseService(IResponseRepository responseRepository, IQuestionRepository questionRepository)
        {
            _responseRepository = responseRepository;
            _questionRepository = questionRepository;
        }

        public async Task SaveResponseAsync(SaveMultipleChoiceResponseDto dto)
        {
            // Fetch the existing QuestionOption from the database if OptionId is provided
            QuestionOption? questionOption = null;

            if (dto.OptionId.HasValue)
            {
                // Fetch the QuestionOption from the repository using the option ID from the DTO
                 questionOption = await _questionRepository.GetOptionByIdAsync(dto.OptionId.Value);

                // Check if the QuestionOption exists and is valid
                if (questionOption == null || questionOption.QuestionId != dto.QuestionId)
                {
                    throw new Exception("Invalid Option: The specified QuestionOption does not exist or does not belong to the specified question.");
                }
            }

            // Create the Response entity
            var response = new Response
            {
                QuestionId = dto.QuestionId,
                DepartmentId = dto.DepartmentId,
                QuestionOption = questionOption, // Set to the fetched QuestionOption
                CreatedAt = DateTime.UtcNow
            };

            // Save the response using the repository
            await _responseRepository.SaveAsync(response);
        }

        public async Task<SaveTextResponseDto> SaveTextResponseAsync(SaveTextResponseDto textResponseDto)
        {
            // Convert DTO to entity
            var textResponse = new Response
            {
                QuestionId = textResponseDto.QuestionId,
                UserId = textResponseDto.UserId,
                ResponseText = textResponseDto.ResponseText,
                DepartmentId = textResponseDto.DepartmentId,
                                CreatedAt = DateTime.UtcNow
            };

            // Use the AddAsync method of the repository
            var savedResponse = await _responseRepository.AddAsync(textResponse);

            // Update the DTO with the generated ResponseId from the saved entity
            

            return textResponseDto;
        }



        public async Task<List<GetResponsesByDepartmentIdDto>> GetResponsesByDepartmentIdAsync(int departmentId)
        {
            // Retrieve the list of Response objects from the repository
            var responses = await _responseRepository.GetResponsesByDepartmentIdAsync(departmentId);

            // Check if responses are null or empty
            if (responses == null || !responses.Any())
            {
                return new List<GetResponsesByDepartmentIdDto>();
            }

            // Map the Response objects to GetResponsesByDepartmentIdDto objects
            var responseDtos = responses.Select(response => new GetResponsesByDepartmentIdDto
            {
                ResponseId = response.ResponseId,
                QuestionId = response.QuestionId,
                DepartmentId = response.DepartmentId,
                DepartmentName = response.Question?.Department?.DepartmentName ?? "Unknown", // Safe navigation and default value
                ResponseText = response.ResponseText,
                OptionId = response.OptionId,
                Text = response.QuestionOption?.Text ?? "No option text",
                Score = response.Score,
                CreatedAt = response.CreatedAt,
                QuestionText = response.Question?.QuestionText ?? "No question text",
                
            }).ToList();

            // Return the list of DTOs
            return responseDtos;
        }


        public async Task ClearResponsesByDepartmentIdAsync(int departmentId)
        {
            await _responseRepository.ClearResponsesByDepartmentIdAsync(departmentId);
        }

        public async Task ClearAllResponsesAsync()
        {
            await _responseRepository.ClearAllResponsesAsync();
        }

        // Method to generate Excel file by departmentId
        public async Task<byte[]> ExportResponsesToExcelAsync(int? departmentId = null)
        {
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            var responses = departmentId.HasValue
                ? await _responseRepository.GetResponsesByDepartmentIdAsync(departmentId.Value)
                : await _responseRepository.GetAllResponsesAsync(); // Assuming method exists to get all responses

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Responses");

            // Add header
            worksheet.Cells[1, 1].Value = "ResponseId";
            worksheet.Cells[1, 2].Value = "QuestionId";
            worksheet.Cells[1, 3].Value = "DepartmentId";
            worksheet.Cells[1, 4].Value = "DepartmentName";
            worksheet.Cells[1, 5].Value = "QuestionText"; // Fixed index
            worksheet.Cells[1, 6].Value = "ResponseText";
            worksheet.Cells[1, 7].Value = "OptionId";
            worksheet.Cells[1, 8].Value = "Text"; // Fixed index
            worksheet.Cells[1, 9].Value = "Score";
            worksheet.Cells[1, 10].Value = "CreatedAt";
            // Populate data
            int row = 2;
            foreach (var response in responses)
            {
                worksheet.Cells[row, 1].Value = response.ResponseId;
                worksheet.Cells[row, 2].Value = response.QuestionId;
                worksheet.Cells[row, 3].Value = response.DepartmentId;
                worksheet.Cells[row, 4].Value = response.Question?.Department?.DepartmentName ?? "Unknown";
                worksheet.Cells[row, 5].Value = response.Question?.QuestionText ?? "No question text";
                worksheet.Cells[row, 6].Value = response.ResponseText;
                worksheet.Cells[row, 7].Value = response.OptionId;
                worksheet.Cells[row, 8].Value = response.QuestionOption?.Text ?? "No Text Option";
                worksheet.Cells[row, 9].Value = response.Score;
                worksheet.Cells[row, 10].Value = response.CreatedAt;
                row++;

            }

            return package.GetAsByteArray();
        }

    }

}
