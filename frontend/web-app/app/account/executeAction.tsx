// eslint-disable-next-line @typescript-eslint/no-unused-vars
const executeAction = async ({ actionFn }: { actionFn: () => Promise<void> }) => {
    try {
      await actionFn();
    } catch (error) {
      console.error("Error executing action:", error);
      alert("Something went wrong. Please try again.");
    }
  };
  