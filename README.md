# LEGIO
### A Roman Legion Command Roguelike — Game Design Document v1.0

---

## 1. Core Concept

You command a Roman legion across a campaign season. Recruit soldiers, train them,
watch them age and rise through the ranks, and lead them into battle on a tactical
grid. Individual soldiers matter — names, histories, scars, deaths — but combat
rewards *coordination*, not just individual stat checks. Lose your legion in
battle, and the run ends — but veterans, perks, and unlocks carry forward into
the next campaign.

**Genre:** Tactics RPG x Management Sim x Roguelike
**Engine:** Unity 3D (URP recommended for stylized lighting on a budget)
**Camera:** Top-down / isometric tactical view for battles; tilted overhead for
the legion management "camp" screens

---

## 2. The Roguelike Loop

### 2.1 Run Structure

A **run** = one campaign season. The legion starts small (an Evocatus and a
handful of raw recruits) and grows across a string of procedurally-selected
battles, each harder than the last, until either:

- **Victory**: the season's final battle (a major engagement against a named
  enemy commander) is won, or
- **Defeat**: the legion is wiped out, routed beyond recovery, or the player
  chooses to retreat from a loss they can't survive.

Either way, the run ends and you return to the **Senate Screen** (meta-progression
hub) before starting a new campaign.

### 2.2 What Resets

- All current soldiers (your roster is wiped — this is the emotional gut-punch
  roguelikes rely on)
- Gold, equipment, formation loadouts
- The campaign map / battle sequence (newly generated)

### 2.3 What Persists (Meta-Progression)

- **Legendary Veterans**: Any soldier who reaches Centurio rank or higher before
  the run ends is inducted into the **Legion Record**. In future runs, they may
  appear as a rare, powerful recruit option — already named, already storied,
  with a one-line legacy blurb generated from their chronicle ("Survived the
  Battle of the Three Hills as a wounded Optio"). Recruiting them again costs
  more, but they start at a much higher rank/stat baseline.
- **Unlocked Perks**: Persistent campaign-start bonuses purchased with a
  meta-currency (**Honor**, earned from runs based on battles won, soldiers
  promoted, and how far you got). Examples:
  - Start with 2 extra recruits
  - Unlock a new Group Action from the start (see Section 4)
  - Reduced training costs
  - A relic item (see Section 6) guaranteed in the first shop
- **Unlocked Cohort Types**: Specialized recruit types (e.g. Sagittarii archers,
  Equites cavalry) start locked and are unlocked permanently via Honor spend.
- **Codex/Bestiary**: Enemy types and formations you've encountered are
  documented for future reference (pure flavor + minor tactical info advantage).

### 2.4 Run Pacing

A campaign season is a **branching path map** (think Slay the Spire) where each
node is one of:
- **Battle** (required, scaling difficulty)
- **Skirmish** (optional, lower risk/reward, good for blooding new recruits)
- **Camp** (rest, train, recruit, no combat)
- **Market** (spend gold on equipment, mercenaries, relics)
- **Event** (narrative choice with risk/reward — a Roman "ink and dice" moment)

---

## 3. Soldiers — The Heart of the Game

### 3.1 Identity

Every soldier is procedurally named and given a one-line background at
recruitment. Stats are rolled but background flavors how their chronicle entries
are phrased (a "gladiator's student" gets different battle-log flavor text than
a "farmer's son").

### 3.2 Core Stats

| Stat | Effect |
|---|---|
| Attack | Base melee damage |
| Defense | Damage reduction |
| Stamina | HP pool + resistance to fatigue penalties in long battles |
| Tactics | Success chance/magnitude bonus when participating in Group Actions |
| Leadership | Required to hold officer ranks; buffs nearby soldiers passively |

### 3.3 Rank Progression

Ranks are earned through experience (combat survival, kills, successful Group
Actions, training). Rank determines:
- Which **Group Actions** a soldier can *call* (initiate) vs. merely *join*
- Passive aura bonuses to nearby soldiers
- Narrative weight — higher ranks get richer chronicle entries and are the ones
  who get inducted into the Legion Record on death/retirement

| Rank | XP Req. | Can Call | Passive |
|---|---|---|---|
| Legionarius | 0 | — | — |
| Signifer | 3 | Shield Wall | +Morale to adjacent units (standard bearer effect) |
| Optio | 8 | Charge | +5% group action success to adjacent units |
| Vexillarius | 15 | Flanking Strike | +10% group action success |
| Centurio | 25 | Testudo | +15% group action success, aura radius +1 |
| Aquilifer | 40 | (all) + Rally | Legion-wide morale bonus while alive |
| Legatus Legionis | 60 | (all) + Grand Maneuver | Legion-wide stat bonus |

*(Evocatus is a unique starting rank — narratively a veteran brought back to
lead the first run's rough recruits. Mechanically equivalent to Centurio.)*

### 3.4 Aging & Death

- Soldiers age each campaign season node that represents "downtime" (Camp nodes,
  inter-battle).
- Past a threshold age (~50), stats begin to decay slightly each tick, and a
  small chance of natural death/retirement triggers.
- **Permadeath in battle is real** — a soldier reduced to 0 HP is dead, full
  stop, chronicle closes with a death entry. This is the emotional core of the
  "build stories" goal: the player should feel Marcus Tiberius's death.
- Wounded-but-surviving soldiers may carry **scars** (minor permanent stat
  trade-offs with flavor text) instead of dying outright on some near-fatal
  hits — a roguelike mercy mechanic that adds character rather than just
  ending it.

### 3.5 The Chronicle

Every soldier has a personal log: recruitment, promotions, training milestones,
battles fought, kills, wounds, and (if applicable) death. This is presented in
a "Legion Chronicle" journal UI between battles. On death, their final entry is
read like an epitaph. On retirement (old age, survives to campaign end), their
full story can be reviewed — and if rank-qualified, they're proposed for
induction into the Legion Record for future runs.

---

## 4. Battle System

### 4.1 Grid

- **Square grid**, individual-unit based (not cohort-abstracted).
- Battlefield size scales with battle importance: skirmishes might be 8x8,
  major battles 14x10 with terrain features (hills for defense bonus, forests
  for ambush/stealth, river crossings as chokepoints).
- Turn-based, side-based initiative (all player units act, then all enemy
  units act, modulated by a speed/initiative stat for ordering within a side).

### 4.2 Solo Actions

Standard tactics-RPG verbs: Move, Attack, Defend (brace, +defense until next
turn), Throw Pilum (one-time ranged opener before melee range closes), Retreat.

### 4.3 Group Actions — The Differentiator

This is the system that delivers "army" feel without abstracting away
individuals. Group Actions require:
1. A **caller** — typically an officer of sufficient rank, standing in a
   qualifying position
2. **Participants** — other soldiers in the required spatial arrangement
   relative to the caller (adjacent, in a line, in a triangle, etc.)
3. A **success check** — influenced by the caller's rank, Tactics stat, and
   experience; higher-rank/higher-XP callers get better magnitude and near-
   guaranteed success, while a green Optio attempting it might fail or
   underperform

| Action | Requirement | Effect | Caller Rank |
|---|---|---|---|
| **Shield Wall** | 3+ soldiers in an unbroken line | Defense ×1.4 for the line this turn, minor counter-damage to attackers | Signifer+ |
| **Charge** | 3 soldiers in a wedge/column moving into one target | Heavy bonus damage to target, scales with participant count, risk of overextension (lower defense next turn) | Optio+ |
| **Flanking Strike** | 2 soldiers attacking the same enemy from non-adjacent sides | Bonus damage + chance to ignore enemy Defense | Vexillarius+ (one of the two must be) |
| **Testudo** | 4+ soldiers in a tight square/cluster | Near-immune to ranged attacks and charge bonus damage; cannot attack this turn | Centurio+ |
| **Rally** | Aquilifer alive and standing | Removes a turn's worth of accumulated fatigue/morale penalties legion-wide | Aquilifer |
| **Grand Maneuver** | Legatus alive, used once per battle | Free repositioning move for up to 5 units without spending their turn | Legatus |

**Success/magnitude formula (concept):**
```
successChance = baseChance(actionType)
              + callerRankBonus
              + (callerTactics * 0.5)
              + (callerExperience / 10)
              - enemyInterferencePenalty (if caller is under threat/flanked)

magnitude = baseMagnitude(actionType) * (1 + successRoll/100)
```

A veteran Centurio calling Charge might guarantee success with a big damage
multiplier; a fresh Optio might succeed only 60% of the time with a modest
bonus — incentivizing the player to *develop* officers specifically to anchor
their best Group Actions, deepening the character-investment loop.

### 4.4 Formation Carry-Over

Pre-battle, the player arranges starting positions on a deployment zone (this
is where the old "wax tablet" planning screen concept lives on) — essentially
choosing initial unit placement so the first turn or two can immediately set up
a Shield Wall or Wedge without spending turns repositioning under fire.

### 4.5 Officers as Force Multipliers

Because Group Actions are rank-gated and quality-scaled by experience, officers
become the soldiers the player most wants to protect and develop — naturally
reinforcing the "this specific soldier matters" emotional goal without needing
to abstract combat away from individuals.

---

## 5. Meta-Progression Hub: The Senate Screen

Between runs:
- Spend **Honor** on permanent unlocks (perks, cohort types, starting bonus
  recruits)
- Review the **Legion Record** — your hall-of-fame veterans from past runs,
  browsable as a memorial/trophy room
- View overall campaign statistics (runs played, total battles won, longest
  surviving soldier, etc.)

---

## 6. Supporting Systems (Lighter Scope, Still Worth Noting)

- **Equipment/Relics**: Gold-purchased gear at Market nodes — a better gladius,
  segmented armor, a standard that boosts Shield Wall, etc. Lost on run end
  unless a "keep one relic" perk is unlocked.
- **Events**: Narrative dice-roll choices between battles ("A local chieftain
  offers grain for safe passage — trust him?") with small stat/gold/recruit
  consequences. Cheap to build, high flavor payoff.
- **Enemy Variety**: Procedurally assembled from enemy "unit pools" per culture
  (Gallic, Germanic, Parthian, etc.) so no two runs' battles feel identical.

---

## 7. Visual & Art Direction

Carrying forward the "parchment and ink" identity from the original web
prototype, adapted for 3D:
- **Battle grid**: stylized low-poly terrain, warm desaturated palette (ochres,
  olive greens, weathered stone), unit silhouettes clear at a glance
- **UI**: parchment-textured panels, Roman serif/Trajan-style display type for
  headers, hand-inked icon style for action buttons
- **Chronicle/Camp screens**: presented as an illuminated manuscript / war
  journal aesthetic — this is where the "story" the player has been building
  gets to breathe

---

## 8. Build Plan / Milestones

1. **Vertical slice**: single battle map, 5 player units, 2 Group Actions
   (Shield Wall, Charge), one enemy type, no meta-progression — prove the
   combat feel
2. **Campaign loop**: branching path map, Camp/Market/Event nodes, full rank
   and chronicle system
3. **Roguelike wrapper**: run-end/run-start flow, Senate Screen, Honor economy,
   Legion Record persistence
4. **Content pass**: full Group Action roster, enemy culture variety, relics,
   events
5. **Polish**: animation, VFX for Group Actions (this is where "watching
   soldiers charge together" should really land), audio, UI final pass

---

## 9. Open Design Questions (For Later Decisions)

- Exact grid size tiers per battle type
- Whether Group Action failure has a *negative* consequence beyond "no bonus"
  (e.g. a failed Charge leaves the group exposed) — recommended yes, for
  tension
- Whether equipment is per-soldier or pooled/legion-wide
- Specific Honor costs/unlock tree shape
