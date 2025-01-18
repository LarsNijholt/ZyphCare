namespace ZyphCare.Users.Models;

/// <summary>
/// Represents the different blood types categorized by the ABO system and Rh factor.
/// </summary>
public enum BloodTypes
{
    /// <summary>
    /// Represents the O Positive (O+) blood type, one of the most common and universal blood types
    /// in the ABO and Rh blood group system. This blood type is often in high demand for transfusions.
    /// </summary>
    OPositive,
    /// <summary>
    /// Represents the O Negative (O-) blood type, known as the universal donor in the ABO and Rh blood group system.
    /// This blood type can be transfused to nearly all patients regardless of their blood type, which makes it critically important in emergency situations.
    /// </summary>
    ONegative,
    /// <summary>
    /// Represents the A Positive (A+) blood type, a common blood group characterized by the presence of
    /// A antigens on red blood cells and the Rh (Rhesus) factor. It is widely required for medical transfusions.
    /// </summary>
    APositive,
    /// <summary>
    /// Represents the A Negative (A-) blood type, a less common blood type in the ABO and Rh blood group system.
    /// This blood type is known for being compatible with specific transfusion requirements.
    /// </summary>
    ANegative,
    /// <summary>
    /// Represents the B Positive (B+) blood type, a relatively common blood type
    /// in the ABO and Rh blood group system. It is compatible for transfusions
    /// with B Positive and AB Positive recipients.
    /// </summary>
    BPositive,
    /// <summary>
    /// Represents the B Negative (B-) blood type, a less common blood group in the ABO and Rh classification system.
    /// Individuals with this blood type can typically donate to others with B Negative or AB Negative blood types.
    /// </summary>
    BNegative,
    /// <summary>
    /// Represents the AB Positive (AB+) blood type, known as the universal recipient for plasma transfusions
    /// in the ABO and Rh blood group system. Individuals with this blood type can receive plasma from all blood types.
    /// </summary>
    AbPositive,
    /// <summary>
    /// Represents the AB Negative (AB-) blood type, a rare blood type in the ABO and Rh blood group system.
    /// Due to its rarity, it is highly valuable for transfusions and donations.
    /// </summary>
    AbNegative
}