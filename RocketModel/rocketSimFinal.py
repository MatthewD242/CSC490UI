from NewData import Data, parse_File 
from TheVectorClass import Vector

import matplotlib.pyplot as plt
import math 
obj = Data('FlightModel-Real.xml')
velVector = Vector(0, 0, 0) 
posVector = Vector(0, 0, 0)
posNext = Vector(0, 0, 0)
velNext = Vector(0, 0, 0)
deltaV = Vector(0, 0, 0)
gravVector = Vector(0, 0, -9.8)

launchVector = Vector(0, 0, 1)
useLaunchV = True

dragCoeff = Data.get_Drag(obj)
currentMass = Data.get_Mass(obj)
dragVector = Vector(dragCoeff, 0, 0)
TimeArray = []
PosArray = []
VelArray = []
altitudes = []


deltaT = Data.get_time(obj)
time = 0.0

TimeArray.append(time)
PosArray.append(0)
VelArray.append(0)

def Thrust(timeMs):
    if timeMs < 600:
        return 14.4
    else:
        return 0.0

while posVector.z >= 0:  # go until the rocket hits the ground
    # Use Launch vector to start computations, velocity thereafter
    if (useLaunchV) :
        dragVector = launchVector * -1
    else :
        dragVector = velVector.unit_vec() * -1

    dragForce = (float(velVector.magnitude()) ** 2.0) * float(dragCoeff)
    #print(dragForce)
    #print(currentMass)
    dragAccMag = float(dragForce) / float(currentMass / 1000.0)
    dragVector = dragVector * dragAccMag
    thrustForce = Thrust(time)  
    accThrust = float(thrustForce) / float(currentMass / 1000.0)

    # Use Launch vector to start computations, velocity thereafter
    if (useLaunchV) :
        thrustAccVector = launchVector
        useLaunchV = False
    else :
        thrustAccVector = velVector.unit_vec()

    thrustAccVector = thrustAccVector * accThrust
    totalAccVector = gravVector + dragVector + thrustAccVector 
    
    velNext = velVector + totalAccVector * (deltaT / 1000.0)
    posNext = posVector + ((velVector + velNext) / 2) * (deltaT / 1000.0)
    time = time + deltaT
    posVector = posNext
    velVector = velNext
    TimeArray.append(time)
    PosArray.append(posVector.z)
    VelArray.append(velVector.z)
    print(posVector.z)



# convert the final position back to feet for display
final_pos = Vector(posVector.x, posVector.y, 0)
altitudes.append(final_pos.y)


# now report on position X & Y, time, etc
print("Range:")
print(final_pos.x)

# Plot altitude vs time
fig2, ax2 = plt.subplots(figsize=(8, 6))
ax2.plot(TimeArray, PosArray)
ax2.set_xlabel('Time')
ax2.set_ylabel('Altitude')
ax2.set_title('Altitude vs Time')
fig2.savefig('altitude.png')
maxAlt = max(PosArray)
print("Max Altitude:")
print(maxAlt)

# Plot velocity vs time
fig3, ax3 = plt.subplots(figsize=(8, 6))
ax3.plot(TimeArray, VelArray)
ax3.set_xlabel('Time')
ax3.set_ylabel('Velocity')
ax3.set_title('Velocity vs Time')
fig3.savefig('velocity.png')
#plt.show()


