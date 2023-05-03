import math

class Vector:

    #Method for setup of the class
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

    #Accessor for class
    def set_val(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

    #Gives the ability to retrieve the values
    def get_val(self):
        return self.x, self.y, self.z
   
    #Calculates the magnitude of the vector
    def magnitude(self):
        magnitude = math.sqrt((self.x * self.x) + (self.y * self.y) + (self.z * self.z))
        return magnitude
    
    #Math for returning the unit vector
    def unit_vec(self):
        mag = self.magnitude()
        x = self.x/mag
        y = self.y/mag
        z = self.z/mag
        return Vector(x, y, z)

    #Overrides the + operator
    def __add__(self, other):
        x = self.x + other.x
        y = self.y + other.y
        z = self.z + other.z
        return Vector(x, y, z)
        
    #Overrides the - operator
    def __sub__(self, other):
        x = self.x - other.x
        y = self.y - other.y
        z = self.z - other.z
        return Vector(x, y, z)

    #Overrides the * operator

    def __mul__(self, other):
        if isinstance(other, (int, float)):  # if other is a scalar
            x = self.x * other
            y = self.y * other
            z = self.z * other
            return Vector(x, y, z)
        elif isinstance(other, Vector):  # if other is a vector
            x = self.y * other.z - self.z * other.y
            y = self.z * other.x - self.x * other.z
            z = self.x * other.y - self.y * other.x
            return Vector(x, y, z)
        else:
            print("Unsupported operand type for *")
            #raise TypeError("Unsupported operand type for *")

    def __truediv__(self, other):
        if isinstance(other, (int, float)):  # if other is a scalar
            x = self.x / other
            y = self.y / other
            z = self.z / other
            return Vector(x, y, z)
        elif isinstance(other, Vector):  # if other is a vector
            x = self.y / other.z - self.z * other.y
            y = self.z / other.x - self.x * other.z
            z = self.x / other.y - self.y * other.x
            return Vector(x, y, z)
        else:
            raise TypeError("Unsupported operand type for /")
    
    #Math for returning Dot Product of 2 Vectors
    def dot(self, other):
        return sum(x*y for x, y in zip(self, other, strict = True))

    @staticmethod
    #Math for 2D Vector
    def math_2d(angle, mag):
        x = math.cos(math.radians(angle)) * mag
        y = math.sin(math.radians(angle)) * mag
        z = 0
        return Vector(x, y, z)










    

