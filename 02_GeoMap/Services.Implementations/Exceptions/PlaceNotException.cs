using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations.Exceptions;
public class PlaceNotExistException(Guid Id) : Exception($"Place with id: {Id} not exist.");
