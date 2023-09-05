﻿using System.ComponentModel.DataAnnotations;

namespace Musicians.Domain.Entities;

public class User
{
    [Key]
    public int Id { get; set; } 
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

}
