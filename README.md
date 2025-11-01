# ThriftService

**Gitflow Workflow Summary**
This project uses Gitflow, a branching strategy designed for organized development and controlled releases.

**Main Branches**
main — Production-ready code

develop — Active development for the next release

**Supporting Branches**
feature/* — New features (branched from develop, merged back into develop)

release/* — Release preparation (branched from develop, merged into main + develop)

hotfix/* — Urgent production fixes (branched from main, merged into main + develop)

**Workflow**
Create feature branch → develop feature → merge back to develop

Create release branch → test & finalize → merge to main + develop

Create hotfix branch if urgent bug in production → fix → merge to main + develop

Gitflow helps maintain clean production code while supporting parallel development and planned releases.
