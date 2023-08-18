# bridge
Starting here 
https://learn.microsoft.com/en-us/dotnet/orleans/tutorials-and-samples/tutorial-1

/*
Parent Grain    Subscriber Manager (one per SB topic)  - SubscriberGrain2 (per partition) - ProcessorGrain (1to1 with SubscriberGrain)
*/

/* Subscriber Manager manages lifetime of SubscriberGrain
- If SubscriberGrain goes down, Subscriber Manager restarts it

Subscriber Manager - for x partitions, create SubscriberGrains GetGrain(x).doWork <- then watch subscriber grains lifecycle??
SubscriberGrain - while(true) readMessage and send to processorGrain -> await processorGrain.process()
ProcessGrain -> Validate Message, build ccsV2 message, put on queue

//When app starts, parentGrain.getShitDone, possibly manage parentGrain in silo
//ParentGrain manages SubscriberManagers
//SubscriberManagers manage SubscriptionGrains
//SubscriptionGrains get one subscription


//Possibly wrap parent grain in a while loop, add is healthy method, call that every five minutes