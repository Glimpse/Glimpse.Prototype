
We follow a simple branching strategy:

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

- Work should be done in a feature branch from **dev**.
  - These branches should follow the naming convention ##-short-dash-delimited-description-of-branch, where ## is the corresponding issue number the branch is addressing.
- Releases will be tagged with, and where appropriate, branched into their own branch which will be named based on the [semantic version](http://semver.org/) of the code at that point in time.
