# Arena Brawl

A site for matchmatching players into Magic the Gathering: Arena "Brawl" game mode


This site was up and running from 10th November through to 1st July when Wizards added the mode into the game client.

See Reddit posts for more info:
- Launch https://www.reddit.com/r/MagicArena/comments/dugzo2/arenabrawlnet_play_brawl_when_you_want/
- Boom in activity https://www.reddit.com/r/MagicArena/comments/e9dvsf/thanks_for_clashing_arenabrawlnet_joking_not/
- Closing down announcement https://www.reddit.com/r/MagicArena/comments/hf9ac3/we_won_brawl_is_now_free_arenabrawlnet_will_soon/


# Technical Information
The site was built in a rush to meet the demands of the commuinity as fast as possible. I picked server side Blazor since it was a new technology that allowed me to get realtime behaviour up and running very fast. This is probably where the benefits of the Blazor ended. The main issue we had with it (and of course in some part due to the rushed implementation) was that the bandwidth and CPU requirement of the server was very high for the load we had. In hindsight I would have picked a standard MVC framework to build this site.
