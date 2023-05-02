from NewData import Data, parse_File
from TheVectorClass import Vector
import matplotlib.pyplot as plt
import math 
import glob

dryMass = Data.get_DryMass
pressure = Data.get_Pressure
mass = Data.get_Mass
drag = Data.get_Drag
modelTime = Data.get_modelTime
massTime = Data.get_massTime
thrustTime = Data.get_thrustTime
velVector = Vector(15, 880, 0) 
posVector = Vector(0,0,0)
posNext = Vector(0, 0, 0)
velNext = Vector(0, 0, 0)
deltaV = Vector(0, 0, 0)
gravVector = Vector(0, -9.8, 0)

dragCoeff =  0.02
currentMass = 1225

TimeArray = []
PosArray = []
VelArray = []
altitudes = []

deltaT = 0.1
time = 0.0 

TimeArray.append(time)
PosArray.append(posVector)
VelArray.append(velVector)

# get a list of all files in the current directory that match the pattern 'FlightModel-*.xml'


while posVector.y >= 0:  # go until the projectile hits the ground
    dragVector = velVector.unit_vec() * -1
    dragForce = (velVector.magnitude() ** 2) * dragCoeff
    dragAccMag = dragForce / currentMass
    dragVector = dragVector * dragAccMag
    totalAccVector = gravVector + dragVector
    newVelVector = velVector + totalAccVector * deltaT
    velNext = velVector + gravVector * deltaT
    posNext = posVector + ((velVector + velNext) / 2) * deltaT
    time = time + deltaT
    posVector = posNext
    velVector = newVelVector
    TimeArray.append(time)
    PosArray.append(posVector)
    VelArray.append(velVector)


# convert the final position back to feet for display
final_pos = Vector(posVector.x, posVector.y, 0)
altitudes.append(final_pos.y)


# now report on position X & Y, time, etc
print("Range:")
print(final_pos.x, final_pos.y)

# Plot the projectile trajectory
fig1, ax1 = plt.subplots(figsize=(8, 6))
ax1.plot([pos.x for pos in PosArray], [pos.y for pos in PosArray])
ax1.set_xlabel('X position')
ax1.set_ylabel('Y position')
ax1.set_title('Projectile Trajectory')
# Save the trajectory plot to a PDF file
fig1.savefig('trajectory.png')

# Plot altitude vs time
fig2, ax2 = plt.subplots(figsize=(8, 6))
altitudes = [pos.y for pos in PosArray]
ax2.plot(TimeArray, altitudes)
ax2.set_xlabel('Time (s)')
ax2.set_ylabel('Altitude')
ax2.set_title('Altitude vs Time')

# Save the altitude vs time plot to a PDF file
fig2.savefig('altitude.png')

# Plot velocity vs time
fig3, ax3 = plt.subplots(figsize=(8, 6))
velocities = [vel.magnitude() for vel in VelArray]
ax3.plot(TimeArray, velocities)
ax3.set_xlabel('Time (s)')
ax3.set_ylabel('Velocity')
ax3.set_title('Velocity vs Time')

# Save the velocity vs time plot to a PDF file
fig3.savefig('velocity.png')
#plt.show()
 