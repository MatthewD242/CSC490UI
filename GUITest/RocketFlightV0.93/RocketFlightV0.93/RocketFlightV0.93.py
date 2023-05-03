# RocketFlight.py
# Rocket Flight Simulator
# Ver 0.90, JWK, 04.25.23
#   Rocket Flight W/O air resistence
# Ver 0.91, JWK, 04.25.23 16:45
#   Add air resistance

from TheVectorClass import Vector
import math 
from matplotlib import pyplot as plt

# mass in grams
emptyMass = 150.0
reactionMass = 200.0

# Linear thrust over 600 ms burn time
# this would be overridden by custom code
def Thrust(timeMs = 0):
    if (timeMs <= 600):
        return 14.71
    else:
        return 0.0
    
# Linear mass loss over 600 ms burn time
# this would be overridden by custom code
def MassTime(timeMs = 0):
    if timeMs <= 600:
        return (emptyMass + reactionMass - reactionMass * 0.001666) / 1000.0
    else:
        return emptyMass / 1000.0

# Vectors for use in simulation
launchVector = Vector(0, 0, 1)
posVector = Vector(0,0,0)
posNext = Vector(0, 0, 0)
velVector = Vector(0, 0, 0)
velNext = Vector(0, 0, 0)
deltaV = Vector(0, 0, 0)
accVector = Vector(0, 0, 0)
gravVector = Vector(0, 0, -9.8) # Z axis flight in 3D
thrustVector = Vector(0, 0, 0) # FORCE of thrust (Newtons)
airResistenceVector = Vector(0, 0, 0 ) # FORCE of air resistence (Newtons)

CDeff = 0.02

# tracking arrays for plots
TimeArray = []
AltArray = []
VelArray = []
AccArray = []

# integration time step MS
deltaT = 100

# flight time MS
time = 0

# max values
maxAlt = 0.0
maxVel = 0.0
maxAcc = 0.0

# begin arrays
TimeArray.append(time)
AltArray.append(posVector.z)
VelArray.append(velVector.magnitude())
AccArray.append(accVector.magnitude())
 
while posVector.z >= 0:  # go until the rocket craters
    # compute acceleration due to thrust plus gravity and air resistence
    airResistMag = CDeff * (velVector.magnitude() ** 2)
    airResistenceVector = (launchVector * -1.0) * airResistMag # force of air resistance
    thrustVector = launchVector * Thrust(time) # force of thrust

    #divide force by mass to get acceleration
    currentMass = MassTime(time)
    accVector = gravVector +  thrustVector / currentMass + airResistenceVector / currentMass  
    velNext = velVector + accVector * (deltaT / 1000.0)  # add acceleration over time span to get next velocity
    posNext = posVector + ((velVector + velNext) / 2) * (deltaT / 1000.0)  # add average velocity over time span to get next position
    time = time + deltaT  # increment time value
    posVector = posNext  # set current pos & vel to next
    velVector = velNext
    launchVector = velVector.unit_vec() # set direction of next thrust computation

    # test for Max values
    if (posVector.z > maxAlt):
        maxAlt = posVector.z
    if (velVector.magnitude() > maxVel):
        maxVel = velVector.magnitude()
    if (accVector.magnitude() > maxAcc):
        maxAcc = accVector.magnitude()

    TimeArray.append(time)  # append values to arrays
    AltArray.append(posVector.z)
    VelArray.append(velVector.magnitude() * velVector.unit_vec().z)
    AccArray.append(accVector.magnitude() * accVector.unit_vec().z)
    # End loop

# now report on max values and plots vs time

print("Max Atitude ", round(maxAlt, 2))
print("Max Velocity ", round(maxVel, 2))
print("Max acceleration ", round(maxAcc,2))

plt.plot(TimeArray, AltArray) #altitude plot
plt.savefig('.\\Altitude.png')
plt.clf()
plt.plot(TimeArray, VelArray) # velocity plot
plt.savefig('.\\Velocity.png')
plt.clf()
plt.plot(TimeArray, AccArray) # acceleration plot
plt.savefig('.\\Acceleration.png')

# END

