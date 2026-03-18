# Commit Message Practices
Commit messages are to be structured as follows:

**{ticketID}:{type}(\{scope\}): description**

Where:
- ticketID (OPTIONAL)
    - Optional, only used if related to a specific ticket ID (**Which should be the case 90% of the time**)
- type
    - Can be one of the following:
        - feat - feature
        - fix - bug fix
        - doc - documentation
        - test - test-related
        - perf - performance-related
        - refactor (self-explanatory)
        - (...)
    - Deviations from the above abbreviations are fine, those are just examples
    - If you have done more than 1 type of work in a single commit, label the type as the most dominant type of work you have done in said commit.
- scope (OPTIONAL)
    - Relates to the design component involved (e.g. API)
- description
    - A short meaningful text describing the changes

Examples:
- 139:feat(ai): Added scouting behaviour for guards
- 512:fix(weapons): Fixed bug causing fully automatic weapons to have an uncapped fire rate
- doc(restructure): Restructured the repository
