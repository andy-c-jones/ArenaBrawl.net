# Arena Brawl

A site for matchmatching players into Magic the Gathering: Arena "Brawl" game mode


This site was up and running from 10th November through to 1st July when Wizards added the mode into the game client.

See Reddit posts for more info:
- Launch https://www.reddit.com/r/MagicArena/comments/dugzo2/arenabrawlnet_play_brawl_when_you_want/
- Boom in activity https://www.reddit.com/r/MagicArena/comments/e9dvsf/thanks_for_clashing_arenabrawlnet_joking_not/
- Closing down announcement https://www.reddit.com/r/MagicArena/comments/hf9ac3/we_won_brawl_is_now_free_arenabrawlnet_will_soon/


# Technical Information
The site was built in a rush to meet the demands of the commuinity as fast as possible. I picked server side Blazor since it was a new technology that allowed me to get realtime behaviour up and running very fast. This is probably where the benefits of the Blazor ended. The main issue we had with it (and of course in some part due to the rushed implementation) was that the bandwidth and CPU requirement of the server was very high for the load we had. In hindsight I would have picked a standard MVC framework to build this site.

I am leaving the repo "as is" but its worth mentioning there is some database integration code and deployment stuff int he pipline yml which was actually never used because we took the decision not to create the concept of user accounts to keep the site simple and cheap to operate.

If you do want to build it then you will need dotnet core 3.0+
then
```
cd ArenaBrawl
dotnet build
dotnet run
```
and you are good to go

### A quick note on secrets
We used Stripe to collect donations, you will see how easy that was in the code! None of the keys in this repo were ever the production secret and I rotated the test secret before publishing publicly ;)

### Server Side Blazor

I think server side blazor is objectively bad. The user wokwokwok on Hacker news sums up my feelings on Server Side Blazor nicely here: https://news.ycombinator.com/item?id=18137920

To quote two of his points that really stood out to me while I was running the site:

> Every UI interaction, from mouse focus, to mouse drag, to click to button down, is sent down the wire to the server to be processed. For high latency situations, this will feel terrible.

> Every client shares the same server cluster. As such, it scales badly; your application may perform well with 10 users, but for 100 users, you have 10x as much server work happening. Not business logic... UI logic. Will it scale? No one knows, but the money is on 'no'. The server scale-out demands will scale with people viewing the website; effectively this means the UI will lag out under load when the server is busy, the exact opposite of modern SPA applications and apps that remain superficially responsive under server load, and compounding issues generated from (1).


# Licence etc...
Feel free to run your own production version if you want but...

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

Standard MIT licence can be found along side this readme

