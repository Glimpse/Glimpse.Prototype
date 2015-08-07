# How to Contribute
Open source projects thrive on contributions from the developer community. Would like to get involved? There is plenty that you can do to help!

## Getting Involved

This is a general guide about how to contribute to Glimpse. It is not a set of hard and fast rules. Any questions, concerns or suggestions should be raised as an [issue here](./issues).

### Submitting Issues

Bugs should be reported in the [GitHub Issue](./issues) tracker if they have not been previously submitted. The best way to get your bug fixed is to be as detailed as you can be about the problem. Providing a minimal project with steps to reproduce the problem is ideal. 

Here are questions you can answer before you file a bug to make sure you're not missing any important information:

1. Did you read the [documentation](./wiki)?
2. Did you include the snippet of broken code in the issue?
3. What are the *EXACT* steps to reproduce this problem?
4. What package versions are you using (you can see these in the `project.json` file)?
5. What operating system are you using?
6. What version of IIS (or your chosen host) are you using?

*GitHub supports [markdown](http://github.github.com/github-flavored-markdown/), so when filing bugs make sure you check the formatting before clicking submit.*

Bugs will be addressed as soon as humanly possible, but please allow ample time. For quicker responses, you may also choose to implement and contribute the bug fix.

### Fixing Issues

Always feel free to fix an issue that you see raised or one that you have encountered yourself. Additionally, for first-timers, we maintains several issues [tagged as Jump In on GitHub](./issues?labels=Jump+In&milestone=&page=1&sort=updated&state=open). If one peaks your interest, feel free to work on it and let us know if you need any help doing so.

 - [Learn more about how "Jump In" issues work](http://nikcodes.com/2013/05/10/new-contributor-jump-in/)

### New Features

For those looking to get more deeply involved, [reach out](http://getglimpse.com/Community) to find out about our current efforts and how you can help. Medium or large contribution should begin by opening a basic pull request or issue so that a discussion can be started. You can describe the contribution you are interested in making, and any initial thoughts on implementation. This will allow the community to discuss and become involved with you from the get go. Find out more information about our development process below.

### Create an Extension

Get the best out of Glimpse by writing your own extension to expose diagnostic data that is meaningful for your applications. Creating extensions is easy, check [the docs](http://getglimpse.com/Custom-Tabs) or reference an [open source extension](http://getglimpse.com/Extensions) to get started.

 - [Creating your own custom extensions and tabs](http://getglimpse.com/Custom-Tabs)
 - [Creating your own custom runtime policies](http://getglimpse.com/Custom-Runtime-Policy)

### Share Glimpse

If you love Glimpse, tell others about it! Present Glimpse at a company tech talk, your local user group or submit a proposal to a conference about how you are using Glimpse or any extensions you may have written.

### Documentation

Documentation is a key differentiator between good projects and _great_ ones. Whether you’re a first time OSS contributor or a veteran, documentation is a great stepping stone to learn our contribution process.

Contributing to Glimpse documentation is dead simple. To make it so easy, we're using Glimpse’s [GitHub Wiki](./wiki) as the entry point for documentation - each page within the docs section of the site has a link to take you straight to the page where you can make changes directly. GitHub Wikis provide an online WYSIWYG interface for adding and editing the docs, completely in browser, using [Markdown](https://daringfireball.net/projects/markdown/).

# Development Process

Work from the core team will be done directly on GitHub. For the most part these contribtions will start as pull requests before being merged in. 

### Core Branches

**Development**
We will do our best to keep `dev` in good shape, with tests passing at all times. But in order to move fast, we will make API changes that your application might not be compatible with. We will do our best to communicate these changes and always version appropriately so you can lock into a specific version if need be.

**Release**
`master` is maintained as the code that was last released and `release` is updated with the latest bits from `dev` that we are intending to release. When prepairing for a release, typically bug fixes will be made directly to `release` and pulled back into `dev`. This allows for normal development to continue on `dev` without affecting the release.

**Working**
When submitting a fix or working on a new feature, we follow a simple branching strategy:
 - Branches should follow the naming convention ##-short-dash-delimited-description-of-branch (where ## is the corresponding issue number the branch is addressing).

```
•
└── master
    ├── release
    │   ├── 2.0-beta
    │   ├── 2.0
    │   └── 2.0.1
    └── dev
        ├── 23-branch-description
        └── 10-branch-description
```

### Pull Requests

The core team will be monitoring for pull requests. When we get one, we'll run some unit & integration tests on it first. From here, we'll need to get another person to sign off on the changes and then merge the pull request. 

*Before* submitting a pull request, please make sure the following is done…

1. Fork the repo and create your branch from `dev` (or `release` if fixing a bug for an upcoming release).
2. If you've added code that should be tested, add tests!
3. If you've changed APIs, update the documentation.
4. Ensure the test suite passes.
5. Make sure your code passes style cop.
6. If you haven't already, complete the CLA.

### Contributor License Agreement (CLA)

In order to accept your pull request, we need you to submit a CLA. You only need to do this once, so once complete, you're good to go. If you are submitting a pull request for the first time, just let us know that you have completed the CLA and we can cross-check with your GitHub username.

 - [Complete your CLA here.](http://getglimpse.com/cla)
 
### Code Conventions

Glimpse follows a loose set of coding conventions. Chiefly among them:

* Ensure all unit tests pass successfully
* Cover additional code with passing unit tests
* Try not to add any additional StyleCop warnings to the compilation process
* Ensure your [Git autocrlf setting](https://help.github.com/articles/dealing-with-line-endings) is true to avoid "whole file" diffs.

## Additional Resources

* [Project Governance Model](http://getglimpse.com/Docs/Governance-Model)
* [General GitHub documentation](http://help.github.com/)
* [GitHub pull request documentation](http://help.github.com/send-pull-requests/)
* [Nightly NuGet Feed](http://www.myget.org/F/glimpsenightly/)
* [Milestone NuGet Feed](http://www.myget.org/F/glimpsemilestone/)
* [Production NuGet Feed](https://nuget.org/api/v2/)

