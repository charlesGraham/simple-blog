using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.DTO
{
  public class RegisterRequestDto
  {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
  }
}