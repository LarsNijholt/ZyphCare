using FastEndpoints;
using ZyphCare.HealthRecords.Entities;

namespace ZyphCare.HealthRecords.Api.Endpoints.GetById;

/// <summary>
/// Provides functionality to map between <see cref="HealthRecord"/> entities and <see cref="Response"/> models.
/// </summary>
public class Mapper : ResponseMapper<Response, HealthRecord>
{
    /// <summary>
    /// Maps a <see cref="HealthRecord"/> entity to a <see cref="Response"/> model.
    /// </summary>
    /// <param name="healthRecord">The <see cref="HealthRecord"/> entity to be mapped.</param>
    /// <returns>A new instance of <see cref="Response"/> populated with data from the provided <see cref="HealthRecord"/> entity.</returns>
    public override Response FromEntity(HealthRecord healthRecord) => new(
    healthRecord.Id,
    healthRecord.PatientId,
    healthRecord.FileName,
    healthRecord.CreatedDate,
    healthRecord.Type);
}