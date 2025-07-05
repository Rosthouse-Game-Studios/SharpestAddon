Cars are neat. They go vroom when you hit the pedal and do it quickly. Driving games are popular for a reason, after all. What's not so nice is having your butt directly connected to the road (or dirt or stone, water, whatever you allow your cars to drive on). 

And ever since the first cars came around, we used some form of suspension to isolate our butts from the bumpy roads (funnily enough, it took some dudes riding down hills for suspension to catch on in bikes). 

Now, a suspension is basically nothing else than a complicated spring. So how hard can it be to create a good feeling suspension in a video game? 

So, I opened up Godot and slapped a basic car together, using the built-in [`VehicleBody3D`](https://docs.godotengine.org/en/stable/classes/class_vehiclebody3d.html#class-vehiclebody3d). Nice! It has everything I need and even comes [`VehicleWheel3D`](https://docs.godotengine.org/en/stable/classes/class_vehiclewheel3d.html) with a support node for the wheels! And look at that, the wheels already have a simple suspension system built in. How convenient. My game is basically already finished (he said, without noticing the hubris). 

Well, that didn't work. Even though I used all default values, the suspension can't actually support the car itself. Well, let's try to fiddle around with some values. The suspension has 3 parameters to set, `Travel`, `Stiffness` and `Max Force`. Just below that, we find 2 damping values, one for compression and one for relaxation.

Just by intuition, I guessed that stiffness should define how much the suspension resists being compressed, and damping should even out that movement. 

After reading the docs, I tried to follow the documentation. But something always felt off. Either the suspension was too bouncy, or too soft, or just flat out didn't work. I thought I was doing everything as suggested by the docs, but nothing felt "right". I thought that maybe I didn't quite understand what the values were actually doing.

After carefully rereading the docs, I got puzzled. It basically just says, "use these values for this type of car, and these for that, have this a bit higher, and please don't actually use this for god’s sake!". I thought that didn't really help me much.

Confused, I returned to the editor and had a good look at the wheel node again. Yes, I did use the suggested numbers. But what did they actually mean? There were some units attached to them (`N/mm` for the stiffness, same for the damping values). How do these actually play with the suspension?

Well, I fear there's only one way to figure this out.

![I'm afraid we need to use Math](https://media1.tenor.com/m/27_dSZxHPcwAAAAd/futurama-math.gif)

## Dust of the math skills

I figured, if I'm ever going to create a good feeling suspension, I should probably understand what it does, how it works and what settings I can play with. So, Bring out the math books, a whiteboard and a calculator!

My go-to math book for everything video game is, of course, the [3D math Primer for Graphics and Game Development](https://gamemath.com/) by Fletcher Dunn and Ian Parberry. Apart from being a fantastic reference work for graphics math, it also contains sections about physics, springs (a suspension is basically just a spring) and a lot more.

In this book, the force a spring exerts is defined by this formula:

f_{net} = f_{r}  + f_{d} = -kx - c\dot{x}

We see that a spring is split into two forces, one being the restorative force and the other being the damping force. We've already encountered a damping parameter, so maybe that's what `c` is in that equation? But we have two values for that, so let's put a pin in that.

We're still missing the travel and stiffness parameters, so let's go searching for these. By the book, `x` should be the traveled distance, so that may be related to the travel. If `c` is related to the damping, that leaves `k` for the stiffness.

Reading a bit further up in that chapter, `k` is defined as

![Definition of stiffness]()

Which is called the spring constant and says how "stiff" a spring is. I think we found our candidate. With `M` being the mass of an object and `T` being time, this is expressed in Newtons. 

Newton isn't really a force that is easy to get an intuition for. At least for me. Reathing further, we learn that an undamped spring has a frequency for a given mass, that can be defined as follows:



The frequency is a lot more useful. If we use Herz (`1Hz = 1/s`, so how many times a second something happens) as a unit, we gain a lot more understanding about what our suspension is doing. If we can set the frequency of our spring, instead of the stiffness, it should be a lot easier to tune our suspension. As it is easier to think "my suspension cycles every 0.25 seconds instead of expressing that in Newtons.

Rearangin that formula, we get a definition of `k` based on the frequency:

![Stiffness by frequency]()

Is that still the same units as before? Let's replace the frequency by its definition. We get:

![Stiffness with frequency replaced by Newton]()

Alright, that looks pretty good. We get our Newtons back. Now we can express the stiffness value as a function of a frequency, which should make configuring a lot easier.

However, that is only half of the equation (quite literally!). We are still missing the damping parameter.

If we read a bit further, we find that the damping coefficient `c` and the stiffness `k` are related to each other through the _damping ratio_. This ratio gives a spring certain qualities, for example how quickly the spring cycle will deterioate. Or in other words, how quickly it stops. It's a model for friction, heath radiation, basically anything that removes energy from the spring.

![Zeta funtion]()

This ratio is expressed with the greek letter zetta (ζ). This value is unit-less, but can take on any value greater than 0. However, most often it's used close to 1. Looking at the following graph gives us a hint as to why:

![Damping with different zeta values]()

Most importantly, with a ζ value of one, our system is critically damped. Meaning, it will deterioate in exactly one cycle. So if we keep our zeta value at 1, we ensure that the spring will compress and decompress exactly once for the frequency, giving us precise control over how "quickly" our suspension should react. That is very helpfull.

If we solve the Zeta function for `c`, we get a function that for a given zeta, mass and stiffness, gives us a usable damping value `c`:

![Damping by Zeta]()

Cool, so now we can express all the values of a spring in terms of 2 more intuitive values. At least for me. We say how quickly our spring reacts by defining its frequency and how quickly it will return to its rest state.

Let's get back to Godot and implement that!

## InspectorPlugin for the car suspension
