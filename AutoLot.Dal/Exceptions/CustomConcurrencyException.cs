﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Dal.Exceptions
{
	public class CustomConcurrencyException : CustomException
	{
		public CustomConcurrencyException() { }

		public CustomConcurrencyException(string message) : base(message) {}

		public CustomConcurrencyException(string messsage, DbUpdateConcurrencyException innerException)
			: base(messsage, innerException) {}
	}
}
